using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using TextMiner.ProviderSchema.PubMed;
using TextMiner.ProviderSchema.PubMed.Search;
using TextMiner.Properties.Settings;

namespace TextMiner.Worker
{
    class PubMed : DataSource
    {
        protected readonly string[] _myns = { "http://tempuri.org/PubMed", "http://tempuri.org/PubMedSearch" };

        public PubMed(int pollingInterval, int timeout, int postPollingInterval, int responseTimeout, int ontogratorTab) 
            : base(ProcessType.PubMed, "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi", pollingInterval, timeout, postPollingInterval, responseTimeout, ontogratorTab) { }

        protected override bool DoWork()
        {
            bool isWork = false;

            // Get the searches we want to send
            List<Search> searches = GetSearches();

            if (searches.Count > 0)
                isWork = true;

            // For each search returned
            foreach (Search search in searches)
            {
                if (!Alive)
                    return false;

                // Zero based index
                int start = search.StartAt;
                int c = search.EndAt;

                // Send a search
                while (c > start && Alive)
                {
                    string url = _serviceUrl + '?' + search.SearchString;
                    url += "&retstart=" + start.ToString();

                    XmlDocument doc = RetrieveWebDocument(url);

                    if (doc == null)
                        continue;

                    eSearchResult searchResults = null;

                    try
                    {
                        searchResults = GetSearchResultsFromXml(doc);
                    }
                    catch {/* Do nothing */}

                    // Maybe we have a PubmedArticle instead
                    if (searchResults == null)
                    {
                        try
                        {
                            PubmedArticleSet articles = GetPubmedArticleSetFromXml(doc);
                            foreach (PubmedArticle article in articles.PubmedArticle)
                            {
                                MinePubmedArticle(article);
                            }
                            // Can't be any more studies to get, so get out of the while loop
                            break;
                        }
                        catch (Exception ex)
                        {
                            EventLogWriter.WriteError("Error serialising PubMed search results:\nSearch ID: {0}\n{1}\n{2}", search.ID, ex, doc.InnerXml);
                            continue;
                        }
                    }

                    TextMiner.ProviderSchema.PubMed.Search.ItemsChoiceType[] choices = searchResults.ItemsElementName;
                    int retmax = 0;

                    // If we get this far, we have search results
                    for (int i = 0; i < choices.Length; i++)
                    {
                        if (!Alive)
                            return false;

                        TextMiner.ProviderSchema.PubMed.Search.ItemsChoiceType choice = choices[i];

                        switch (choice)
                        {
                            case TextMiner.ProviderSchema.PubMed.Search.ItemsChoiceType.Count:
                                {
                                    int actualc = int.Parse((string)searchResults.Items[i]);
                                    if (actualc < c)
                                        c = actualc;
                                    break;
                                }
                            case TextMiner.ProviderSchema.PubMed.Search.ItemsChoiceType.RetStart:
                                {
                                    start = int.Parse((string)searchResults.Items[i]);
                                    break;
                                }
                            case TextMiner.ProviderSchema.PubMed.Search.ItemsChoiceType.RetMax:
                                {
                                    retmax = int.Parse((string)searchResults.Items[i]);
                                    if ((start + retmax) > c)
                                        retmax = c - start;
                                    break;
                                }
                            case TextMiner.ProviderSchema.PubMed.Search.ItemsChoiceType.IdList:
                                {
                                    IdList idList = (IdList)searchResults.Items[i];

                                    if (retmax <= 0)
                                        break;

                                    string ids = idList.Id[0];
                                    for (int j = 1; j < retmax; j++)
                                    {
                                        ids += ',' + idList.Id[j];
                                    }

                                    start += retmax;

                                    try
                                    {
                                        XmlDocument articleXml = RetrieveWebDocument(string.Format("http://eutils.ncbi.nlm.nih.gov/entrez/eutils/efetch.fcgi?db=pubmed&id={0}&retmode=xml", ids));
                                        if (articleXml == null)
                                            continue;

                                        PubmedArticleSet articles = GetPubmedArticleSetFromXml(articleXml);
                                        foreach (PubmedArticle article in articles.PubmedArticle)
                                        {
                                            MinePubmedArticle(article);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        EventLogWriter.WriteError("Error serialising PubMed article:\nSearch ID: {0}\n{1}\n{2}", search.ID, ex, doc.InnerXml);
                                        continue;
                                    }

                                    Sleep(PostPollingInterval);

                                    break;
                                }
                        }
                    }

                    Sleep(PostPollingInterval);
                }
            }

            return isWork;
        }

        private void MinePubmedArticle(PubmedArticle article)
        {
            Article pubmedDoc = article.MedlineCitation.Article;
            int month = 0, year = 0;
            string pubdate = string.Empty;

            if(pubmedDoc.ArticleDate != null && pubmedDoc.ArticleDate.Length > 0)
            {
                pubdate = string.Format("{2}-{1}-{0}", pubmedDoc.ArticleDate[0].Day, pubmedDoc.ArticleDate[0].Month, pubmedDoc.ArticleDate[0].Year);
                try
                {
                    month = int.Parse(pubmedDoc.ArticleDate[0].Month);
                }
                catch {/* Do nothing */}

                try
                {
                    year = int.Parse(pubmedDoc.ArticleDate[0].Year);
                }
                catch {/* Do nothing */}
            }

            string docUrl = "http://www.ncbi.nlm.nih.gov/pubmed/" + article.MedlineCitation.PMID;

            string text = pubmedDoc.ArticleTitle;
            if (pubmedDoc.Abstract != null)
                text += '\n' + pubmedDoc.Abstract.AbstractText;

            string authors = string.Empty;
            if (pubmedDoc.AuthorList != null && pubmedDoc.AuthorList.Author != null)
            {
                foreach (Author author in pubmedDoc.AuthorList.Author)
                {
                    if (author.Items == null)
                        continue;
                    
                    string lastName, initials, suffix;
                    lastName = initials = suffix = string.Empty;

                    for (int i = 0; i < author.ItemsElementName.Length; i++)
                    {
                        ItemsChoiceType2 type = author.ItemsElementName[i];
                        switch (type)
                        {
                            case ItemsChoiceType2.LastName:
                                lastName = (string)author.Items[i];
                                break;
                            case ItemsChoiceType2.Initials:
                                initials = (string)author.Items[i];
                                break;
                            case ItemsChoiceType2.Suffix:
                                suffix = (string)author.Items[i];
                                break;
                        }
                    }

                    if (suffix != string.Empty)
                        suffix = ' ' + suffix;
                    if (initials != string.Empty)
                        initials = ", " + initials;
                    if (authors != string.Empty)
                        authors += ';';
                    
                    authors += lastName + suffix + initials;
                }
            }

            // Document Links
            string docLinks = string.Empty;
            if (article.PubmedData != null && article.PubmedData.ArticleIdList != null)
            {
                foreach (ArticleId artid in article.PubmedData.ArticleIdList)
                {
                    if (artid.IdType != ArticleIdIdType.pubmed)
                    {
                        string strid = string.Format("{0}:{1}", artid.IdType, artid.Value);
                        if (docLinks == string.Empty)
                            docLinks = strid;
                        else
                            docLinks += "," + strid;
                    }
                }
            }

            // Try to find links in the abstract
            if (pubmedDoc.Abstract != null && pubmedDoc.Abstract.AbstractText != null)
            {
                string abst = pubmedDoc.Abstract.AbstractText;
                int findid = abst.IndexOf("NCT", 0);
                while (findid >= 0)
                {
                    string nctid = abst.Substring(findid, 11);
                    int i;
                    if (int.TryParse(nctid.Substring(3), out i))
                    {
                        if (docLinks == string.Empty)
                            docLinks = "?" + nctid;
                        else
                            docLinks += ",?" + nctid;
                    }

                    findid = abst.IndexOf("NCT", ++findid);
                }
            }

            if (year > 0 && month > 0)
                InsertHit(_ontogratorTab, 4, article.MedlineCitation.PMID, docLinks, month.ToString("00") + '/' + year.ToString("0000"), docUrl, pubmedDoc.ArticleTitle, authors, pubdate, string.Empty);

            if (Repository.Configuration.LogSearchInformation)
                EventLogWriter.WriteInformation("Mining {0} document ID {1} at {2}", _processType, article.MedlineCitation.PMID, docUrl);

            Dictionary<int, Dictionary<string, Hit>> hits = MineText(text);
            foreach (int pane in hits.Keys)
            {
                foreach (Hit h in hits[pane].Values)
                {
                    string keywords, matchedSentences;
                    NormaliseHitKeywordsAndMatchedSentences(h, out keywords, out matchedSentences);
                    InsertHit(_ontogratorTab, pane, article.MedlineCitation.PMID, docLinks, h.OntologyID, docUrl, pubmedDoc.ArticleTitle, authors, keywords, matchedSentences);
                }
            }
        }

        protected eSearchResult GetSearchResultsFromXml(XmlDocument doc)
        {
            XmlElement root = doc["eSearchResult"];
            root.SetAttribute("xmlns", _myns[1]);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                XmlSerializer x = new XmlSerializer(typeof(eSearchResult), _myns[1]);
                return (eSearchResult)x.Deserialize(ms);
            }
        }

        protected PubmedArticleSet GetPubmedArticleSetFromXml(XmlDocument doc)
        {
            XmlElement root = doc["PubmedArticleSet"];
            root.SetAttribute("xmlns", _myns[0]);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                XmlSerializer x = new XmlSerializer(typeof(PubmedArticleSet), _myns[1]);
                return (PubmedArticleSet)x.Deserialize(ms);
            }
        }
    }
}
