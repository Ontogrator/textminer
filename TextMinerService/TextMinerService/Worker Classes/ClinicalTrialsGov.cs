using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using TextMiner.ProviderSchema.ClinicalTrialsGov.ClinicalStudy;
using TextMiner.ProviderSchema.ClinicalTrialsGov.SearchResults;
using TextMiner.Properties.Settings;

namespace TextMiner.Worker
{
    class ClinicalTrialsGov : DataSource
    {
        protected readonly string[] _myns = { "http://tempuri.org/ClinicalTrialsGovClinicalStudy", "http://tempuri.org/ClinicalTrialsGovSearchResults" };

        public ClinicalTrialsGov(int pollingInterval, int timeout, int postPollingInterval, int responseTimeout, int ontogratorTab)
            : base(ProcessType.ClinicalTrialsGov, "http://clinicaltrials.gov/search", pollingInterval, timeout, postPollingInterval, responseTimeout, ontogratorTab) { }

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

                // Send a search
                while (c > start && Alive)
                {
                    string url = _serviceUrl + '?' + search.SearchString;
                    url += "&start=" + start.ToString();

                    XmlDocument doc = RetrieveWebDocument(url);

                    if (doc == null)
                        continue;

                    search_results searchResults = null;

                    try
                    {
                        searchResults = GetSearchResultsFromXml(doc);
                    }
                    catch (Exception){/* Do nothing */}

                    // Maybe we have a clinical_study instead
                    if (searchResults == null)
                    {
                        try
                        {
                            clinical_study study = GetClinicalStudyFromXml(doc);
                            MineClinicalStudy(study);
                            // Can't be any more studies to get, so get out of the while loop
                            break;
                        }
                        catch (Exception ex)
                        {
                            EventLogWriter.WriteError("Error serialising ClinicalTrialsGov search results:\nSearch ID: {0}\n{1}\n{2}", search.ID, ex, doc.InnerXml);
                            continue;
                        }
                    }

                    int actualc = searchResults.count;
                    if (actualc < c)
                        c = actualc;

                    if (actualc <= 0)
                        break;

                    // If we get this far, we have search results
                    foreach (search_resultsClinical_study searchResult in searchResults.clinical_study)
                    {
                        if (!Alive)
                            return false;

                        start = searchResult.order;
                        if (start > c)
                            break;

                        try
                        {
                            XmlDocument studyXml = RetrieveWebDocument(searchResult.url + "?displayxml=true");
                            clinical_study study = GetClinicalStudyFromXml(studyXml);
                            MineClinicalStudy(study);
                        }
                        catch (Exception ex)
                        {
                            EventLogWriter.WriteError("Error serialising ClinicalTrialsGov study:\nSearch ID: {0}\n{1}\n{2}", search.ID, ex, doc.InnerXml);
                            continue;
                        }

                        Sleep(PostPollingInterval);
                    }

                    start++;

                    Sleep(PostPollingInterval);
                }
            }

            return isWork;
        }

        private void MineClinicalStudy(clinical_study study)
        {
            int year = 0, month = 0;

            if (study.firstreceived_date != null)
            {
                if (study.firstreceived_date.ToUpper().Contains("JAN"))
                    month = 1;
                else if (study.firstreceived_date.ToUpper().Contains("FEB"))
                    month = 2;
                else if (study.firstreceived_date.ToUpper().Contains("MAR"))
                    month = 3;
                else if (study.firstreceived_date.ToUpper().Contains("APR"))
                    month = 4;
                else if (study.firstreceived_date.ToUpper().Contains("MAY"))
                    month = 5;
                else if (study.firstreceived_date.ToUpper().Contains("JUN"))
                    month = 6;
                else if (study.firstreceived_date.ToUpper().Contains("JUL"))
                    month = 7;
                else if (study.firstreceived_date.ToUpper().Contains("AUG"))
                    month = 8;
                else if (study.firstreceived_date.ToUpper().Contains("SEP"))
                    month = 9;
                else if (study.firstreceived_date.ToUpper().Contains("OCT"))
                    month = 10;
                else if (study.firstreceived_date.ToUpper().Contains("NOV"))
                    month = 11;
                else if (study.firstreceived_date.ToUpper().Contains("DEC"))
                    month = 12;

                try
                {
                    year = int.Parse(study.firstreceived_date.Substring(study.firstreceived_date.Length - 4));
                }
                catch {/* Do nothing */}
            }

            string sid;
            if (study.id_info != null)
                sid = study.id_info.nct_id;
            else
                sid = "UNKNOWN";

            string docUrl = "http://clinicaltrials.gov/show/" + sid;

            string text = study.official_title;
            text += "." + study.brief_title;
            if (study.brief_summary != null)
                text += "." + study.brief_summary.textblock;
            if (study.detailed_description != null)
                text += "." + study.detailed_description.textblock;
            text += "." + study.study_design;
            text += "." + study.study_type;
            text += "." + study.phase;
            text += "." + study.overall_status;

            if (study.primary_outcome != null)
            {
                foreach (primary_outcome po in study.primary_outcome)
                {
                    text += "." + po.description;
                    text += "." + po.measure;
                }
            }

            if (study.secondary_outcome != null)
            {
                foreach (secondary_outcome so in study.secondary_outcome)
                {
                    text += "." + so.description;
                    text += "." + so.measure;
                }
            }

            if (study.arm_group != null)
            {
                foreach (arm_group arm in study.arm_group)
                {
                    text += "." + arm.arm_group_label;
                    text += "." + arm.arm_group_type;
                    text += "." + arm.description;
                }
            }

            if (study.intervention != null)
            {
                foreach(intervention i in study.intervention)
                {
                    text += "." + i.intervention_name;
                    text += "." + i.intervention_type;
                    if (i.other_name != null)
                    {
                        foreach (string s in i.other_name)
                        {
                            text += "." + s;
                        }
                    }
                    text += "." + i.description;
                }
            }

            if (study.eligibility != null)
            {
                if (study.eligibility.criteria != null)
                {
                    text += "." + study.eligibility.criteria.textblock;
                }
            }

            string authors = string.Empty;
            if (study.overall_official != null)
            {
                foreach (overall_official official in study.overall_official)
                {
                    if(authors != string.Empty)
                        authors += ';';

                    authors += string.Format("{0} ({1}, {2})", official.last_name, official.role, official.affiliation);
                }
            }

            // Document Links
            string docLinks = string.Empty;
            if (study.id_info != null)
            {
                if (study.id_info.nct_alias != null)
                {
                    foreach (string strid in study.id_info.nct_alias)
                    {
                        if (docLinks == string.Empty)
                            docLinks = "alias:" + strid;
                        else
                            docLinks += ",alias:" + strid;
                    }
                }

                if (study.id_info.org_study_id != null)
                {
                    if (docLinks == string.Empty)
                        docLinks = "orgstudy:" + study.id_info.org_study_id;
                    else
                        docLinks += ",orgstudy:" + study.id_info.org_study_id;
                }

                if (study.id_info.secondary_id != null)
                {
                    foreach (string strid in study.id_info.secondary_id)
                    {
                        if (docLinks == string.Empty)
                            docLinks = "alt:" + strid;
                        else
                            docLinks += ",alt:" + strid;
                    }
                }
            }

            if (year > 0 && month > 0)
                InsertHit(_ontogratorTab, 4, sid, docLinks, month.ToString("00") + '/' + year.ToString("0000"), docUrl, study.official_title, authors, study.firstreceived_date, string.Empty);

            if (Repository.Configuration.LogSearchInformation)
                EventLogWriter.WriteInformation("Mining {0} document ID {1} at {2}", _processType, sid, docUrl);

            Dictionary<int, Dictionary<string, Hit>> hits = MineText(text);
            foreach (int pane in hits.Keys)
            {
                foreach (Hit h in hits[pane].Values)
                {
                    if (!Alive)
                        return;

                    string keywords, matchedSentences;
                    NormaliseHitKeywordsAndMatchedSentences(h, out keywords, out matchedSentences);
                    InsertHit(_ontogratorTab, pane, sid, docLinks, h.OntologyID, docUrl, study.official_title, authors, keywords, matchedSentences);
                }
            }
        }

        protected search_results GetSearchResultsFromXml(XmlDocument doc)
        {
            XmlElement root = doc["search_results"];
            root.SetAttribute("xmlns", _myns[1]);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                XmlSerializer x = new XmlSerializer(typeof(search_results), _myns[1]);
                return (search_results)x.Deserialize(ms);
            }
        }

        protected clinical_study GetClinicalStudyFromXml(XmlDocument doc)
        {
            XmlElement root = doc["clinical_study"];
            root.SetAttribute("xmlns", _myns[0]);

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                XmlSerializer x = new XmlSerializer(typeof(clinical_study), _myns[0]);
                return (clinical_study)x.Deserialize(ms);
            }
        }
    }
}
