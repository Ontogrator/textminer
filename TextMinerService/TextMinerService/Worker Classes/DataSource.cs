using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;
using System.Xml;
using System.Net;
using System.IO;
using TextMiner.PdfToText;
using TextMiner.TextCleaner;
using TextMiner.SentenceSplitter;
using System.Xml.Serialization;
using TextMiner.ProviderSchema;
using TextMiner.Properties.Settings;
using System.Globalization;

namespace TextMiner.Worker
{
    abstract class DataSource : Common
    {
        protected readonly int PostPollingInterval;
        protected readonly int _ontogratorTab;
        protected readonly string _serviceUrl;

        public DataSource(ProcessType processType, string serviceUrl, int pollingInterval, int timeout, int postPollingInterval, int responseTimeout, int ontogratorTab)
            : base(processType, pollingInterval, timeout, responseTimeout)
        {
            PostPollingInterval = postPollingInterval;
            _serviceUrl = serviceUrl;
            _ontogratorTab = ontogratorTab;
        }

        public override void Start()
        {
            EventLogWriter.WriteInformation("Thread ID {0} information:\nData Source: {1}\nService URL: {2}\nPage Polling Interval: {3}\nOntogrator Tab: {4}", Thread.CurrentThread.ManagedThreadId, _processType, _serviceUrl, PostPollingInterval, _ontogratorTab);

            base.Start();
        }

        protected List<Search> GetSearches()
        {
            List<Search> searches = new List<Search>();

            using (DataSet ds = new DataSet())
            {
                using (MySqlConnection conn = new MySqlConnection(Repository.Configuration.Database.ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandTimeout = _responseTimeout;
                            cmd.CommandText = "GetSearch";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("processTypeName", _processType.ToString()));

                            using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                            {
                                adp.Fill(ds);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogWriter.WriteError("Error occurred retrieving searches: {0}", ex);
                    }
                }

                foreach (DataTable table in ds.Tables)
                {
                    try
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Search search = new Search();
                            search.ID = (int)row["ID"];
                            search.SearchString = (string)row["SearchString"];
                            search.StartAt = (int)row["StartAt"];

                            object o = row["EndAt"];
                            if(o != null && !(o is DBNull))
                                search.EndAt = (int)o;

                            searches.Add(search);
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogWriter.WriteError("Error occurred reading retrieved searches: {0}", ex);
                    }
                }
            }

            return searches;
        }

        protected XmlDocument HttpPost(string uri, string pars, int responseTimeout)
        {
            XmlDocument ret;

            WebRequest req = WebRequest.Create(uri);
            req.Timeout = (responseTimeout) > 0 ? responseTimeout:System.Threading.Timeout.Infinite;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(pars);
            req.ContentLength = bytes.Length;
            using (Stream s = req.GetRequestStream())
            {
                s.Write(bytes, 0, bytes.Length);
            }

            WebResponse resp = req.GetResponse();
            using (Stream rs = resp.GetResponseStream())
            {
                ret = new XmlDocument();
                ret.Load(rs);
            }

            return ret;
        }

        protected XmlDocument RetrieveWebDocument(string uri)
        {
            if (Repository.Configuration.LogSearchInformation)
                EventLogWriter.WriteInformation("Sending URI: {0}", uri);

            XmlDocument doc = null;

            try
            {
                WebRequest req = WebRequest.Create(new Uri(uri));
                WebResponse resp = req.GetResponse();
                using (Stream rs = resp.GetResponseStream())
                {
                    doc = new XmlDocument();
                    doc.Load(rs);
                }
            }
            catch (Exception ex)
            {
                EventLogWriter.WriteError("Error occurred during web document retrieval:\nURI: {0}\n{1}", uri, ex);
            }

            return doc;
        }

        protected string RetrieveWebString(string uri, Encoding encoding)
        {
            if (Repository.Configuration.LogSearchInformation)
                EventLogWriter.WriteInformation("Sending URI: {0}", uri);

            string s = null;

            try
            {
                WebRequest req = WebRequest.Create(new Uri(uri));
                WebResponse resp = req.GetResponse();
                using (Stream rs = resp.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(rs, encoding))
                    {
                        s = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogWriter.WriteError("Error occurred during web string retrieval:\nURI: {0}\n{1}", uri, ex);
            }

            return s;
        }

        protected byte[] RetrieveWebBytes(string uri)
        {
            if (Repository.Configuration.LogSearchInformation)
                EventLogWriter.WriteInformation("Sending URI: {0}", uri);

            List<byte> bytes = new List<byte>();

            try
            {
                WebRequest req = WebRequest.Create(new Uri(uri));
                WebResponse resp = req.GetResponse();
                using (Stream rs = resp.GetResponseStream())
                {
                    using (BinaryReader reader = new BinaryReader(rs))
                    {
                        bool ok = true;
                        while (ok)
                        {
                            if (!Alive)
                                return null;

                            try
                            {
                                byte b = reader.ReadByte();
                                bytes.Add(b);
                            }
                            catch (EndOfStreamException)
                            {
                                ok = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogWriter.WriteError("Error occurred during web bytes retrieval:\nURI: {0}\n{1}", uri, ex);
            }

            return bytes.ToArray();
        }

        protected Dictionary<int, Dictionary<string, Hit>> MineText(byte[] text, Encoding encoder)
        {
            string s = encoder.GetString(text);
            return MineText(s);
        }

        protected Dictionary<int, Dictionary<string, Hit>> MineText(string text)
        {
            Dictionary<int, Dictionary<string, Hit>> hits = new Dictionary<int, Dictionary<string, Hit>>();

            if (!Alive)
                return hits;

            // TextCleaner went down.
            //try
            //{
            //    TextCleanerClient tcc = new TextCleanerClient();
            //    s = tcc.cleanTextASCII(text);
            //}
            //catch (Exception ex)
            //{
            //    EventLogWriter.WriteError("Error occurred during text clean: {0}", ex);
            //    return hits;
            //}

            //if (!Alive)
            //    return hits;

            // SentenceSplitter went down.
            //try
            //{
            //    SentenceSplitterClient ssc = new SentenceSplitterClient();
            //    sentences = ssc.splitIntoSentences(s);
            //}
            //catch (Exception ex)
            //{
            //    EventLogWriter.WriteError("Error occurred during sentence split: {0}", ex);
            //    return hits;
            //}

            string[] sentences = text.Split(new char[] { '.', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string docSentence in sentences)
            {
                if (!Alive)
                    return hits;

                string sentence = docSentence.Trim();
                if (sentence == string.Empty)
                    continue;

                sentence = CleanString(sentence);

                // Search for custom hits (Miscellaneous Pane)
                // I know, not generic, but this stuff is vital to show what Ontogrator can do...
                // Just remove this crap once a decent ontology goes in Pane 4
                // BEGIN CRAP
                Dictionary<string, string[]> customOntology = new Dictionary<string, string[]>();
                customOntology.Add("Phase I", new string[]{ "Phase 1", "Phase I " });
                customOntology.Add("Phase II", new string[]{ "Phase 2", "Phase II ", "Phase IIa", "Phase IIb" });
                customOntology.Add("Phase II/III", new string[]{ "Phase 2/3", "Phase II/III", "Phase 2/Phase 3" });
                customOntology.Add("Phase III", new string[]{ "Phase 3", "Phase III " });
                customOntology.Add("Phase IV", new string[]{ "Phase 4", "Phase IV " });
                customOntology.Add("Open", new string[] { " open ", "open label" });
                customOntology.Add("Blind", new string[] { "Blind" });
                customOntology.Add("Double-blind", new string[] { "Double-blind", "Double blind" });
                customOntology.Add("Triple-blind", new string[] { "Triple-blind", "Triple blind" });
                customOntology.Add("Randomization", new string[] { "Random" });

                TextMinerServiceSettingsMinerOntology customOntObject = new TextMinerServiceSettingsMinerOntology();
                customOntObject.OntogratorPane = 4;

                foreach (string custOntID in customOntology.Keys)
                {
                    string[] custOntSynonyms = customOntology[custOntID];

                    foreach (string custOntSynonym in custOntSynonyms)
                    {
                        int pos = sentence.IndexOf(custOntSynonym, StringComparison.CurrentCultureIgnoreCase);

                        if(pos >= 0)
                        {
                            string tempontid = custOntID;
                            string custkey = sentence.Substring(pos, custOntSynonym.Length);

                            Hit h = GetHit(hits, customOntObject, ref tempontid);

                            List<string> matchedSentences;
                            if (!h.Keywords.TryGetValue(custkey, out matchedSentences))
                            {
                                matchedSentences = new List<string>();
                                h.Keywords.Add(custkey, matchedSentences);
                            }
                            if (!matchedSentences.Contains(sentence))
                                matchedSentences.Add(sentence);

                            break;
                        }
                    }
                }
                // END CRAP

                if (Repository.Configuration.Miners == null)
                    continue;

                foreach (TextMinerServiceSettingsMiner miner in Repository.Configuration.Miners)
                {
                    if (!Alive)
                        return hits;

                    if (!miner.Enabled || miner.Name == MinerName.Unsupported)
                        continue;

                    XmlDocument hit = null;
                    int tries = 0;
                    string errorMsg = string.Empty;
                    while (hit == null && tries <= miner.RetriesOnError)
                    {
                        if (tries++ > 0)
                            Sleep(miner.IntervalOnRetry);

                        try
                        {
                            hit = HttpPost(
                                miner.Uri,
                                string.Format(miner.Arguments, Uri.EscapeUriString(sentence)),
                                miner.ResponseTimeout
                                );
                        }
                        catch (Exception ex)
                        {
                            errorMsg += string.Format("TRY {0}:\n{1}\n", tries, ex);
                            continue;
                        }
                    }
                    if (hit == null)
                    {
                        EventLogWriter.WriteError("Error occurred during HTTP POST:\nURI: {0}\nArguments: {1}\nSentence: {2}\n{3}", miner.Uri, miner.Arguments, sentence, errorMsg);
                        continue;
                    }

                    switch (miner.Name)
                    {
                        case MinerName.NERCTerminizer:
                            {
                                TerminizerResult result = null;
                                try
                                {
                                    result = GetTerminizerResultFromXml(hit);
                                }
                                catch (Exception ex)
                                {
                                    EventLogWriter.WriteError("Error occurred serialising from Terminizer POST:\nHit: {0}\n{1}", hit.InnerXml, ex);
                                }

                                if (result != null)
                                {
                                    // Build tokens for keywords
                                    Dictionary<int, string> tokens = new Dictionary<int, string>();

                                    foreach (object o in result.Items)
                                    {
                                        if (!(o is Token))
                                            continue;

                                        Token token = (Token)o;
                                        if (token.indexSpecified && token.RawText != null)
                                        {
                                            tokens[token.index] = token.RawText;
                                        }
                                    }

                                    foreach (object o in result.Items)
                                    {
                                        if (!(o is MatchedTermList))
                                            continue;
                                        
                                        MatchedTermList terms = (MatchedTermList)o;

                                        foreach (MatchedTermListMatchedTerm term in terms.MatchedTerm)
                                        {
                                            if (miner.Ontologies == null)
                                                continue;

                                            string ontid = term.Accession.Substring(0, term.Accession.IndexOf(':'));
                                            
                                            TextMinerServiceSettingsMinerOntology ont = null;
                                            foreach (TextMinerServiceSettingsMinerOntology ontology in miner.Ontologies)
                                            {
                                                if (ontology.ID == ontid)
                                                {
                                                    ont = ontology;
                                                    break;
                                                }
                                            }
                                            if (ont == null)
                                                continue;

                                            string ontologyID = term.Accession;

                                            // Create or get the hit object for this pane and ontology ID
                                            Hit h = GetHit(hits, ont, ref ontologyID);

                                            // Add any new keywords
                                            if (term.TokenIndices != null)
                                            {
                                                string[] indices = term.TokenIndices.Split(',');
                                                foreach (string index in indices)
                                                {
                                                    int i;
                                                    if (int.TryParse(index, out i))
                                                    {
                                                        try
                                                        {
                                                            List<string> matchedSentences;
                                                            if (!h.Keywords.TryGetValue(tokens[i], out matchedSentences))
                                                            {
                                                                matchedSentences = new List<string>();
                                                                h.Keywords.Add(tokens[i], matchedSentences);
                                                            }
                                                            if (!matchedSentences.Contains(sentence))
                                                                matchedSentences.Add(sentence);
                                                        }
                                                        catch {/* Do nothing */}
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case MinerName.NCBOAnnotator:
                            {
                                success result = null;
                                try
                                {
                                    result = GetNCBOAnnotatorResultFromXml(hit);
                                }
                                catch (Exception ex)
                                {
                                    EventLogWriter.WriteError("Error occurred serialising from Annotator POST:\nHit: {0}\n{1}", hit.InnerXml, ex);
                                }

                                if (result != null && result.data != null && result.data.annotatorResultBean != null && result.data.annotatorResultBean.annotations != null)
                                {
                                    foreach (successDataAnnotatorResultBeanAnnotationBean annotation in result.data.annotatorResultBean.annotations)
                                    {
                                        successDataAnnotatorResultBeanAnnotationBeanConcept concept = annotation.concept;
                                        if (concept != null)
                                        {
                                            if (miner.Ontologies == null)
                                                continue;

                                            TextMinerServiceSettingsMinerOntology ont = null;
                                            foreach (TextMinerServiceSettingsMinerOntology ontology in miner.Ontologies)
                                            {
                                                if (ontology.ID == concept.localOntologyId)
                                                {
                                                    ont = ontology;
                                                    break;
                                                }
                                            }
                                            if (ont == null)
                                                continue;

                                            string ontologyID = concept.fullId;

                                            // Create or get the hit object for this pane and ontology ID
                                            Hit h = GetHit(hits, ont, ref ontologyID);

                                            // Add any new keywords
                                            if (annotation.context != null && annotation.context.@class == "mgrepContextBean" && annotation.context.term != null)
                                            {
                                                List<string> matchedSentences;
                                                if (!h.Keywords.TryGetValue(annotation.context.term.name, out matchedSentences))
                                                {
                                                    matchedSentences = new List<string>();
                                                    h.Keywords.Add(annotation.context.term.name, matchedSentences);
                                                }
                                                if(!matchedSentences.Contains(sentence))
                                                    matchedSentences.Add(sentence);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
            }

            return hits;
        }

        private Hit GetHit(Dictionary<int, Dictionary<string, Hit>> hits, TextMinerServiceSettingsMinerOntology ontology, ref string ontologyID)
        {
            Hit h;

            // Do any predefined string manipulation on the ontology ID
            if (ontology.MatchText != null)
            {
                foreach (TextMinerServiceSettingsMinerOntologyMatchText matchText in ontology.MatchText)
                {
                    ontologyID = ontologyID.Replace(matchText.ResultTextID, matchText.DBTextID);
                }
            }

            Dictionary<string, Hit> onthits;
            if (!hits.TryGetValue(ontology.OntogratorPane, out onthits))
            {
                onthits = new Dictionary<string, Hit>();
                hits.Add(ontology.OntogratorPane, onthits);
            }

            if (!onthits.TryGetValue(ontologyID, out h))
            {
                h = new Hit(ontologyID);
                onthits.Add(ontologyID, h);
            }

            return h;
        }

        protected void InsertHit(int tab, int pane, string documentID, string documentLinks, string ontologyID, string documentUrl, string documentTitle, string documentAuthors, string keywords, string sentence)
        {
            using (MySqlConnection conn = new MySqlConnection(Repository.Configuration.Database.ConnectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = _responseTimeout;
                        cmd.CommandText = "InsertHit";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("tabid", tab));
                        cmd.Parameters.Add(new MySqlParameter("paneid", pane));
                        cmd.Parameters.Add(new MySqlParameter("documentID", documentID));
                        cmd.Parameters.Add(new MySqlParameter("ontologyID", ontologyID));
                        cmd.Parameters.Add(new MySqlParameter("documentUrl", documentUrl));
                        cmd.Parameters.Add(new MySqlParameter("documentLinks", documentLinks));
                        cmd.Parameters.Add(new MySqlParameter("matchedKeywords", keywords));
                        cmd.Parameters.Add(new MySqlParameter("fullMatchedSentence", sentence));
                        cmd.Parameters.Add(new MySqlParameter("documentTitle", documentTitle));
                        cmd.Parameters.Add(new MySqlParameter("documentAuthors", documentAuthors));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    EventLogWriter.WriteError("Error occurred storing hit:\ntabid: {0}\npaneid: {1}\ndocumentID: {2}\nontologyID: {3}\ndocumentUrl: {4}\ndocumentTitle: {5}\n\ndocumentAuthors: {6}\n{7}", tab, pane, documentID, ontologyID, documentUrl, documentTitle, documentAuthors, ex);
                }
            }
        }

        protected TerminizerResult GetTerminizerResultFromXml(XmlDocument doc)
        {
            string ns = "http://tempuri.org/Terminizer";

            XmlElement root = doc["TerminizerResult"];
            root.SetAttribute("xmlns", ns);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                XmlSerializer x = new XmlSerializer(typeof(TerminizerResult), ns);
                return (TerminizerResult)x.Deserialize(ms);
            }
        }

        protected success GetNCBOAnnotatorResultFromXml(XmlDocument doc)
        {
            string ns = "http://tempuri.org/NCBOAnnotator";

            XmlElement root = doc["success"];
            root.SetAttribute("xmlns", ns);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                XmlSerializer x = new XmlSerializer(typeof(success), ns);
                return (success)x.Deserialize(ms);
            }
        }

        protected void NormaliseHitKeywordsAndMatchedSentences(Hit h, out string keywords, out string matchedSentences)
        {
            Dictionary<string, string> allMatchedSentences = new Dictionary<string, string>();

            keywords = string.Empty;

            List<string> sortedKeywords = new List<string>(h.Keywords.Keys);
            sortedKeywords.Sort(StringComparer.CurrentCultureIgnoreCase);

            for (int i = sortedKeywords.Count - 1; i >= 0; i--)
            {
                string keyword = sortedKeywords[i];

                List<string> listMatchedSentences = h.Keywords[keyword];

                if (keywords == string.Empty)
                    keywords = keyword;
                else
                    keywords += "," + keyword;

                foreach (string matchedSentence in listMatchedSentences)
                {
                    string underlinedSentence;
                    if (!allMatchedSentences.TryGetValue(matchedSentence, out underlinedSentence))
                    {
                        underlinedSentence = matchedSentence;
                        allMatchedSentences.Add(matchedSentence, underlinedSentence);
                    }

                    int keypos = underlinedSentence.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase);
                    while (keypos >= 0)
                    {
                        bool doneAlready = false;
                        if (keypos >= 3)
                            doneAlready = (underlinedSentence.Substring(keypos - 3, 3) == "<u>");

                        if (!doneAlready)
                        {
                            underlinedSentence = underlinedSentence.Insert(keypos, "<u>");
                            keypos += keyword.Length + 3;
                            underlinedSentence = underlinedSentence.Insert(keypos, "</u>");
                            keypos += 4;
                        }
                        else
                        {
                            keypos = underlinedSentence.IndexOf("</u>", keypos);
                            if (keypos > 0)
                                keypos += 4;
                        }

                        // Find next
                        keypos = underlinedSentence.IndexOf(keyword, keypos, StringComparison.CurrentCultureIgnoreCase);
                    }

                    allMatchedSentences[matchedSentence] = underlinedSentence;
                }
            }

            matchedSentences = string.Empty;
            foreach (string underlinedSentence in allMatchedSentences.Values)
            {
                matchedSentences += underlinedSentence + "\n";
            }
        }

        private string CleanString(string s)
        {
            string sD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < sD.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(sD[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(sD[i]);
                }
            }

            return StripIllegalXMLChars(sb.ToString().Normalize(NormalizationForm.FormKC));
        }

        private string StripIllegalXMLChars(string s)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c <= 0x7f)
                    sb.Append(c);
            }

            return System.Security.SecurityElement.Escape(sb.ToString());
        }
    }
}
