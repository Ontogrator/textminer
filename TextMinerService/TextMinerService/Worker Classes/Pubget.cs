using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using TextMiner.ProviderSchema.Pubget;
using TextMiner.PdfToText;
using TextMiner.TextCleaner;
using TextMiner.SentenceSplitter;
using TextMiner.Properties.Settings;

namespace TextMiner.Worker
{
    class Pubget : DataSource
    {
        protected readonly string _myns = "http://tempuri.org/ClinicalTrialsGovClinicalStudy";

        public Pubget(int pollingInterval, int timeout, int postPollingInterval, int responseTimeout, int ontogratorTab)
            : base(ProcessType.Pubget, "http://pubget.com/developer/search", pollingInterval, timeout, postPollingInterval, responseTimeout, ontogratorTab) { }

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

                // 1 based index
                int start = search.StartAt + 1;
                int c = search.EndAt;
                if (c < int.MaxValue)
                    c++;

                int page = Convert.ToInt32(Math.Floor(Convert.ToDouble(search.StartAt) / 10)) + 1;

                // Send a search
                while (c > start && Alive)
                {
                    string url = _serviceUrl + '?' + search.SearchString;
                    url += "&page=" + page.ToString();

                    XmlDocument doc = RetrieveWebDocument(url);

                    if (doc == null)
                        continue;

                    int actualc = int.Parse(doc["count"].InnerText);
                    if (actualc < c)
                        c = actualc;

                    if (c <= 0)
                        break;

                    articles searchResults = null;

                    try
                    {
                        searchResults = GetArticlesFromXml(doc);
                    }
                    catch (Exception ex)
                    {
                        EventLogWriter.WriteError("Error serialising Pubget search results:\nSearch ID: {0}\n{1}\n{2}", search.ID, ex, doc.InnerXml);
                        continue;
                    }

                    // If we get this far, we have search results
                    foreach (articlesArticle searchResult in searchResults.article)
                    {
                        if (!Alive)
                            return isWork;

                        byte[] contents = null;

                        if (!searchResult.pdf.StartsWith("no access"))
                        {
                            byte[] bytes = RetrieveWebBytes(searchResult.pdf);

                            if (bytes != null)
                            {
                                PdfToTextClient pttc = new PdfToTextClient();
                                contents = pttc.pdfToText(bytes);
                            }
                        }

                        // Use the abstract if no full text available
                        if (contents == null)
                        {
                            string text = searchResult.title + '\n' + searchResult.@abstract;
                            contents = Encoding.UTF8.GetBytes(text);
                        }

                        string docId = searchResult.id.ToString();
                        int year = 0, month = 0;

                        try
                        {
                            year = int.Parse(searchResult.date.Substring(0, 4));
                        }
                        catch {/* Do nothing */}

                        try
                        {
                            month = int.Parse(searchResult.date.Substring(6, 2));
                        }
                        catch {/* Do nothing */}

                        string docUrl = "http://pubget.com/paper/" + docId;

                        string authors = string.Empty;
                        if (searchResult.authors != null)
                        {
                            foreach (string author in searchResult.authors)
                            {
                                if (authors != string.Empty)
                                    authors += ';';

                                authors += author;
                            }
                        }

                        // Document Links
                        string docLinks = string.Empty;
                        if(searchResult.html != null)
                            docLinks = string.Format("<a href=\"{0}\">{0}</a>", searchResult.html);

                        // Try to find links in the abstract
                        if (searchResult.@abstract != null)
                        {
                            string abst = searchResult.@abstract;
                            int findid = abst.IndexOf("NCT", 0);
                            while (findid >= 0)
                            {
                                string nctid = abst.Substring(findid, 11);
                                int i;
                                if (int.TryParse(nctid.Substring(3), out i))
                                {
                                    if (docLinks == string.Empty)
                                        docLinks += "?" + nctid;
                                    else
                                        docLinks += ",?" + nctid;
                                }

                                findid = abst.IndexOf("NCT", ++findid);
                            }
                        }

                        if (year > 0 && month > 0)
                            InsertHit(_ontogratorTab, 4, docId, docLinks, month.ToString("00") + '/' + year.ToString("0000"), docUrl, searchResult.title, authors, searchResult.date, string.Empty);

                        if (Repository.Configuration.LogSearchInformation)
                            EventLogWriter.WriteInformation("Mining {0} document ID {1} at {2}", _processType, docId, docUrl);

                        Dictionary<int, Dictionary<string, Hit>> hits = MineText(contents, Encoding.UTF8);

                        foreach (int pane in hits.Keys)
                        {
                            foreach (Hit h in hits[pane].Values)
                            {
                                string keywords, matchedSentences;
                                NormaliseHitKeywordsAndMatchedSentences(h, out keywords, out matchedSentences);
                                InsertHit(_ontogratorTab, pane, docId, docLinks, h.OntologyID, docUrl, searchResult.title, authors, keywords, matchedSentences);
                            }
                        }

                        start++;
                        if (start > c)
                            break;

                        Sleep(PostPollingInterval);
                    }
                    
                    // 10 results per page
                    if (start > 0)
                    {
                        decimal p = start / 10;
                        page = (int)Math.Ceiling(p);
                    }

                    Sleep(PostPollingInterval);
                }
            }

            return isWork;
        }

        protected articles GetArticlesFromXml(XmlDocument doc)
        {
            XmlElement root = doc["articles"];
            root.SetAttribute("xmlns", _myns);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                XmlSerializer x = new XmlSerializer(typeof(articles), _myns);
                return (articles)x.Deserialize(ms);
            }
        }
    }
}
