using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace TextMinerTweak
{
    /// <summary>
    /// Configuration file maintenance
    /// </summary>
    static class Settings
    {
        /// <summary>
        /// The filename of the configuration file
        /// </summary>
        static private readonly string Filename = AppDomain.CurrentDomain.BaseDirectory + "TextMinerTweak.settings.xml";

        /// <summary>
        /// The MySQL DB string
        /// </summary>
        static public string DBConnectionString;

        static public bool SSHEnabled;
        static public string SSHHost;
        static public string SSHUserID;
        static public string SSHPassword;
        static public string SSHPort;
        static public string SSHLocalPort;
        static public string SSHForwardingHost;
        static public string SSHRemotePort;

        /// <summary>
        /// Pane 1 OWL Filename
        /// </summary>
        static public string Pane01Filename;

        /// <summary>
        /// Pane 2 OWL Filename
        /// </summary>
        static public string Pane02Filename;

        /// <summary>
        /// Pane 3 OWL Filename
        /// </summary>
        static public string Pane03Filename;

        /// <summary>
        /// Pane 4 OWL Filename
        /// </summary>
        static public string Pane04Filename;


        /// <summary>
        /// Static constructor loads the settings
        /// </summary>
        static Settings()
        {
            Reload();
        }

        /// <summary>
        /// Static constructor loads the settings
        /// </summary>
        static public void Reload()
        {
            // Setup defaults
            DBConnectionString = "Database=ontogrator;Data Source=localhost;User Id=root;Password=";
            Pane01Filename = ".owl";
            Pane02Filename = ".owl";
            Pane03Filename = ".owl";
            Pane04Filename = ".owl";

            try
            {
                // If the file does not exist save a fresh version with default values
                if (!File.Exists(Filename))
                    Save();

                using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fs);

                    XmlElement root = doc["TextMinerTweak"];
                    XmlElement settings = root["Settings"];

                    XmlElement s;
                    s = settings["DBConnectionString"];
                    DBConnectionString = s.InnerText;

                    s = settings["SSHEnabled"];
                    SSHEnabled = bool.Parse(s.InnerText);

                    s = settings["SSHHost"];
                    SSHHost = s.InnerText;

                    s = settings["SSHUserID"];
                    SSHUserID = s.InnerText;

                    s = settings["SSHPassword"];
                    SSHPassword = s.InnerText;

                    s = settings["SSHPort"];
                    SSHPort = s.InnerText;

                    s = settings["SSHLocalPort"];
                    SSHLocalPort = s.InnerText;

                    s = settings["SSHForwardingHost"];
                    SSHForwardingHost = s.InnerText;

                    s = settings["SSHRemotePort"];
                    SSHRemotePort = s.InnerText;

                    s = settings["Pane01Filename"];
                    Pane01Filename = s.InnerText;

                    s = settings["Pane02Filename"];
                    Pane02Filename = s.InnerText;

                    s = settings["Pane03Filename"];
                    Pane03Filename = s.InnerText;

                    s = settings["Pane04Filename"];
                    Pane04Filename = s.InnerText;
                }
            }
            catch {/* Do nothing */}
        }

        /// <summary>
        /// Saves the configuration file
        /// </summary>
        static public void Save()
        {
            try
            {
                using (FileStream fs = new FileStream(Filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlWriter writer = null;

                    try
                    {
                        // Setup the XML log file
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;
                        writer = XmlWriter.Create(fs, settings);
                        writer.WriteStartDocument();

                        writer.WriteStartElement("TextMinerTweak");
                        writer.WriteStartElement("Settings");

                        writer.WriteStartElement("DBConnectionString");
                        writer.WriteString(DBConnectionString);
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHEnabled");
                        writer.WriteString(SSHEnabled.ToString());
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHHost");
                        writer.WriteString(SSHHost);
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHUserID");
                        writer.WriteString(SSHUserID);
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHPassword");
                        writer.WriteString(SSHPassword);
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHPort");
                        writer.WriteString(SSHPort);
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHLocalPort");
                        writer.WriteString(SSHLocalPort);
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHForwardingHost");
                        writer.WriteString(SSHForwardingHost);
                        writer.WriteEndElement();

                        writer.WriteStartElement("SSHRemotePort");
                        writer.WriteString(SSHRemotePort);
                        writer.WriteEndElement();

                        writer.WriteStartElement("Pane01Filename");
                        writer.WriteString(Pane01Filename);
                        writer.WriteEndElement();

                        writer.WriteStartElement("Pane02Filename");
                        writer.WriteString(Pane02Filename);
                        writer.WriteEndElement();

                        writer.WriteStartElement("Pane03Filename");
                        writer.WriteString(Pane03Filename);
                        writer.WriteEndElement();

                        writer.WriteStartElement("Pane04Filename");
                        writer.WriteString(Pane04Filename);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                        writer.WriteEndElement();

                        writer.WriteEndDocument();
                    }
                    finally
                    {
                        if (writer != null)
                            writer.Close();
                    }
                }
            }
            catch {/* Do nothing */}
        }
    }
}
