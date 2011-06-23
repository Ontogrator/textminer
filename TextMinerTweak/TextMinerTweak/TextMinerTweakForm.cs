using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using Tamir.SharpSsh.jsch;

namespace TextMinerTweak
{
    public partial class TextMinerTweakForm : Form
    {
        /// <summary>
        /// Our SSH session should it be required
        /// </summary>
        private JSch _jsch = null;
        private Session _sshSession = null;

        public TextMinerTweakForm()
        {
            InitializeComponent();

            dbConnBox.Text = Settings.DBConnectionString;
            sshCheck.Checked = Settings.SSHEnabled;
            hostBox.Text = Settings.SSHHost;
            userIDBox.Text = Settings.SSHUserID;
            passwordBox.Text = Settings.SSHPassword;
            portBox.Text = Settings.SSHPort;
            localPortBox.Text = Settings.SSHLocalPort;
            forHostBox.Text = Settings.SSHForwardingHost;
            remotePortBox.Text = Settings.SSHRemotePort;
            pane01FileBox.Text = Settings.Pane01Filename;
            pane02FileBox.Text = Settings.Pane02Filename;
            pane03FileBox.Text = Settings.Pane03Filename;
            pane04FileBox.Text = Settings.Pane04Filename;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Settings.DBConnectionString = dbConnBox.Text;
            Settings.SSHEnabled = sshCheck.Checked;
            Settings.SSHHost = hostBox.Text;
            Settings.SSHUserID = userIDBox.Text;
            Settings.SSHPassword = passwordBox.Text;
            Settings.SSHPort = portBox.Text;
            Settings.SSHLocalPort = localPortBox.Text;
            Settings.SSHForwardingHost = forHostBox.Text;
            Settings.SSHRemotePort = remotePortBox.Text;
            Settings.Pane01Filename = pane01FileBox.Text;
            Settings.Pane02Filename = pane02FileBox.Text;
            Settings.Pane03Filename = pane03FileBox.Text;
            Settings.Pane04Filename = pane04FileBox.Text;
            Settings.Save();

            base.OnClosing(e);
        }

        private void pane01Button_Click(object sender, EventArgs e)
        {
            try
            {
                ImportOntology(dbConnBox.Text, pane01FileBox.Text, 1);
                MessageBox.Show("Successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unsuccessful.\n{0}", ex));
            }
        }

        private void pane02Button_Click(object sender, EventArgs e)
        {
            try
            {
                ImportOntology(dbConnBox.Text, pane02FileBox.Text, 2);
                MessageBox.Show("Successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unsuccessful.\n{0}", ex));
            }
        }

        private void pane03Button_Click(object sender, EventArgs e)
        {
            try
            {
                ImportOntology(dbConnBox.Text, pane03FileBox.Text, 3);
                MessageBox.Show("Successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unsuccessful.\n{0}", ex));
            }
        }

        private void pane04Button_Click(object sender, EventArgs e)
        {
            try
            {
                ImportOntology(dbConnBox.Text, pane04FileBox.Text, 4);
                MessageBox.Show("Successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unsuccessful.\n{0}", ex));
            }
        }

        private void ImportOntology(string dbConn, string filename, int paneId)
        {
            Dictionary<string, OntologyEntry> entries = new Dictionary<string, OntologyEntry>();

            string idKey = null;
            bool headerDone = false;

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            foreach (XmlNode n in doc.DocumentElement.ChildNodes)
            {
                if (!(n is XmlElement))
                    continue;

                XmlElement el = (XmlElement)n;

                if (!headerDone && el.Name == "owl:DatatypeProperty")
                {
                    XmlAttribute a = el.Attributes["rdf:ID"];
                    if (a != null)
                    {
                        idKey = a.InnerText;
                        headerDone = true;
                    }
                }

                if (el.Name != "owl:Class")
                    continue;
                else
                    headerDone = true;

                XmlAttribute idXml = el.Attributes["rdf:about"];
                if (idXml == null)
                    idXml = el.Attributes["rdf:ID"];
                if (idXml == null)
                    idXml = el.Attributes["rdf:resource"];                

                if (idXml == null)
                    continue;

                string id = idXml.InnerText;
                string altID = id;

                if (idKey != null)
                {
                    XmlElement idKeyXml = el[idKey];
                    if (idKeyXml == null)
                        continue;

                    altID = idKeyXml.InnerText;
                }

                XmlElement l = el["rdfs:label"];
                if (l == null)
                    continue;

                string label = l.InnerText;

                OntologyEntry entry = new OntologyEntry(id, label, altID);
                entries.Add(id, entry);

                XmlNodeList subClasses = el.GetElementsByTagName("rdfs:subClassOf");
                foreach (XmlNode node in subClasses)
                {
                    XmlNode c = node["owl:Class"];
                    if (c == null)
                        c = node;

                    XmlAttribute a = c.Attributes["rdf:about"];
                    if (a == null)
                        a = c.Attributes["rdf:ID"];
                    if (a == null)
                        a = c.Attributes["rdf:resource"];

                    if (a != null)
                    {
                        string parentID = a.InnerText;
                        if (parentID.StartsWith("#"))
                            parentID = parentID.Substring(1);

                        if (!entry.Parents.Contains(parentID))
                            entry.Parents.Add(parentID);
                    }
                }

                XmlNodeList synonyms = el.GetElementsByTagName("Synonym");
                foreach (XmlNode node in synonyms)
                {
                    entry.Synonyms.Add(node.InnerText);
                }

                List<XmlNodeList> oboSynonyms = new List<XmlNodeList>();
                oboSynonyms.Add(el.GetElementsByTagName("oboInOwl:hasSynonym"));
                oboSynonyms.Add(el.GetElementsByTagName("oboInOwl:hasExactSynonym"));
                oboSynonyms.Add(el.GetElementsByTagName("oboInOwl:hasNarrowSynonym"));
                oboSynonyms.Add(el.GetElementsByTagName("oboInOwl:hasBroadSynonym"));
                oboSynonyms.Add(el.GetElementsByTagName("oboInOwl:hasRelatedSynonym"));

                foreach (XmlNodeList oboSynonym in oboSynonyms)
                {
                    foreach (XmlNode node in oboSynonym)
                    {
                        if (!(node is XmlElement))
                            continue;

                        XmlElement mysyn = (XmlElement)node;
                        foreach (XmlNode syn in mysyn.GetElementsByTagName("oboInOwl:Synonym"))
                        {
                            if (!(syn is XmlElement))
                                continue;

                            XmlElement elsyn = (XmlElement)syn;

                            foreach (XmlNode synlab in elsyn.GetElementsByTagName("rdfs:label"))
                            {
                                entry.Synonyms.Add(synlab.InnerText);
                            }
                        }
                    }
                }
            }

            Persist(dbConn, entries, paneId);
        }

        private void Persist(string dbConn, Dictionary<string, OntologyEntry> entries, int paneId)
        {
            OntologyEntry rootEntry = new OntologyEntry("<!-- ROOT -->", "<!-- ROOT -->", "<!-- ROOT -->");

            foreach (OntologyEntry entry in entries.Values)
            {
                int p = 0;
                foreach (string parentID in entry.Parents)
                {
                    OntologyEntry parent;
                    if (!entries.TryGetValue(parentID, out parent))
                        continue;
                    p++;

                    parent.Children.Add(entry);
                }

                if (p == 0)
                {
                    rootEntry.Children.Add(entry);
                    continue;
                }
            }

            int left, right;
            int nest = 1;
            int depth = 0;
            left = nest++;
            NestIt(rootEntry, ref nest, depth+1);
            right = nest;
            rootEntry.LeftRights.Add(new int[] { left, right, depth });

            try
            {
                if(Settings.SSHEnabled)
                    OpenSSH();

                using (MySqlConnection conn = new MySqlConnection(dbConn))
                {
                    conn.Open();

                    string query;

                    // Clear the ontology
                    query = "DELETE FROM OntologyEntries WHERE Pane = {0};";
                    query += "DELETE FROM OntologyRelations WHERE Pane = {0};";
                    query += "DELETE FROM OntologySynonyms WHERE Pane = {0};";

                    query = string.Format(query, paneId);

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }

                    // Input the ontology
                    foreach (OntologyEntry entry in entries.Values)
                    {
                        DoEntry(conn, paneId, entry);
                    }
                }
            }
            finally
            {
                CloseSSH();
            }
        }

        private void NestIt(OntologyEntry entry, ref int nest, int depth)
        {
            foreach (OntologyEntry child in entry.Children)
            {
                int left, right;

                left = nest++;
                NestIt(child, ref nest, depth+1);
                right = nest++;

                child.LeftRights.Add(new int[] { left, right, depth });
            }
        }

        private void DoEntry(MySqlConnection conn, int paneId, OntologyEntry entry)
        {
            string query = string.Empty;

            string id = entry.ID.Replace("'", "''");

            int parentCount = entry.Parents.Count;

            foreach (int[] lr in entry.LeftRights)
            {
                if (lr[2] == 1 && parentCount > 0)// If depth is top then no parents
                    parentCount--;

                query += string.Format("INSERT INTO OntologyRelations VALUES ({0}, '{1}',{2},{3},{4});", paneId, id, lr[0].ToString(), lr[1].ToString(), lr[2].ToString());
            }

            query += string.Format("INSERT INTO OntologyEntries VALUES ({0}, '{1}','{2}','{3}','{4}',{5},{6});", paneId, id, entry.AltID.Replace("'", "''"), entry.Label.Replace("'", "''"), entry.Definition.Replace("'", "''"), parentCount, entry.Children.Count);

            query += string.Format("INSERT INTO OntologySynonyms VALUES ({0}, '{1}','{2}');", paneId, id, entry.Label.Replace("'", "''"));
            foreach (string synonym in entry.Synonyms)
            {
                query += string.Format("INSERT INTO OntologySynonyms VALUES ({0}, '{1}','{2}');", paneId, id, synonym.Replace("'", "''"));
            }

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        private void customButton_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, OntologyEntry> entries = new Dictionary<string, OntologyEntry>();

                // Published Dates
                OntologyEntry ent;
                string top;
                string child;
                top = "Published Date";
                OntologyEntry pubdate = new OntologyEntry(top, top, top);
                entries.Add(top, pubdate);

                int yearStart = 1900, yearEnd = 2100;

                for (int i = yearStart; i <= yearEnd; i++)
                {
                    string yrid = i.ToString("0000");
                    OntologyEntry yrentry = new OntologyEntry(yrid, yrid, yrid);
                    yrentry.Parents.Add(top);
                    entries.Add(yrid, yrentry);

                    for (int j = 1; j <= 12; j++)
                    {
                        string mnid = j.ToString("00") + '/' + yrid;

                        DateTime d = DateTime.ParseExact("01/" + mnid, "dd/MM/yyyy", null);

                        OntologyEntry mnentry = new OntologyEntry(mnid, mnid, mnid);
                        mnentry.Parents.Add(yrid);
                        mnentry.Synonyms.Add(d.ToString("MMM"));
                        mnentry.Synonyms.Add(d.ToString("MMMM"));
                        entries.Add(mnid, mnentry);
                    }
                }

                // Phase

                top = "Phase";
                ent = new OntologyEntry(top, top, top);
                entries.Add(top, ent);

                child = "Phase I";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Phase 1");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                child = "Phase II";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Phase 2");
                ent.Synonyms.Add("Phase IIa");
                ent.Synonyms.Add("Phase IIb");
                ent.Synonyms.Add("Phase 2/3");
                ent.Synonyms.Add("Phase II/III");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                child = "Phase II/III";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Phase 2");
                ent.Synonyms.Add("Phase IIa");
                ent.Synonyms.Add("Phase IIb");
                ent.Synonyms.Add("Phase 2/3");
                ent.Synonyms.Add("Phase 3");
                ent.Synonyms.Add("Phase 2/Phase 3");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                child = "Phase III";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Phase 3");
                ent.Synonyms.Add("Phase 2/3");
                ent.Synonyms.Add("Phase II/III");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                child = "Phase IV";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Phase 4");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                // Blinding

                top = "Blinding";
                ent = new OntologyEntry(top, top, top);
                entries.Add(top, ent);

                child = "Open";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Open label");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                child = "Blind";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Blinded");
                ent.Synonyms.Add("Single-blind");
                ent.Synonyms.Add("Single blind");
                ent.Synonyms.Add("Single-blinded");
                ent.Synonyms.Add("Single blinded");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                child = "Double-blind";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Double-blinded");
                ent.Synonyms.Add("Double blind");
                ent.Synonyms.Add("Double blinded");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                child = "Triple-blind";
                ent = new OntologyEntry(child, child, child);
                ent.Synonyms.Add("Triple-blinded");
                ent.Synonyms.Add("Triple blind");
                ent.Synonyms.Add("Triple blinded");
                ent.Parents.Add(top);
                entries.Add(child, ent);

                // Randomisation

                top = "Randomization";
                ent = new OntologyEntry(top, top, top);
                ent.Synonyms.Add("Randomized");
                ent.Synonyms.Add("Randomised");
                ent.Synonyms.Add("Random");
                ent.Synonyms.Add("Randomisation");
                entries.Add(top, ent);

                Persist(dbConnBox.Text, entries, 4);

                MessageBox.Show("Successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unsuccessful.\n{0}", ex));
            }
        }

        protected void OpenSSH()
        {
            try
            {
                //Create a new SSH session
                _jsch = new JSch();
                _sshSession = _jsch.getSession(
                    Settings.SSHUserID,
                    Settings.SSHHost,
                    int.Parse(Settings.SSHPort));

                _sshSession.setHost(Settings.SSHHost);
                _sshSession.setPassword(Settings.SSHPassword);

                UserInfo ui = new JschUserInfo();
                _sshSession.setUserInfo(ui);

                // Connect
                _sshSession.connect();

                //Set port forwarding on the opened session
                _sshSession.setPortForwardingL(
                    int.Parse(Settings.SSHLocalPort),
                    Settings.SSHForwardingHost,
                    int.Parse(Settings.SSHRemotePort));

                if (!_sshSession.isConnected())
                    throw new Exception("SSH Session did not connect.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Could not start due to SSH Error:\n{0}", ex));
                return;
            }
        }
        
        protected void CloseSSH()
        {
            if (_sshSession != null && _sshSession.isConnected())
            {
                _sshSession.disconnect();
                _sshSession = null;
                _jsch = null;
            }
        }
    }

    public class JschUserInfo : UserInfo
    {
        private String passwd;
        public String getPassword() { return passwd; }

        public bool promptYesNo(String str)
        {
            return true;
        }

        public String getPassphrase() { return null; }
        public bool promptPassphrase(String message) { return true; }
        public bool promptPassword(String message) { return true; }
        public void showMessage(String message) { }
    }
}
