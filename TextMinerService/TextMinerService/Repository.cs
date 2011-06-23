using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;
using TextMiner.Properties.Settings;

namespace TextMiner
{
    /// <summary>
    /// Configuration file maintenance
    /// Basically a wrapper class for TextMiner.Properties.Settings.TextMinerServiceSettings
    /// </summary>
    static class Repository
    {
        /// <summary>
        /// The filename of the configuration file
        /// </summary>
        static private readonly string Filename = AppDomain.CurrentDomain.BaseDirectory + "TextMinerService.settings.xml";

        /// <summary>
        /// Our configuration
        /// </summary>
        public static TextMinerServiceSettings Configuration = null;

        /// <summary>
        /// Static constructor loads the settings
        /// </summary>
        static Repository()
        {
            try
            {
                using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlSerializer x = new XmlSerializer(typeof(Properties.Settings.TextMinerService));
                    Properties.Settings.TextMinerService serviceConfig = (Properties.Settings.TextMinerService)x.Deserialize(fs);
                    Configuration = serviceConfig.Settings;
                }
            }
            catch(Exception ex)
            {
                EventLogWriter.WriteError("Error reading settings file {0}:\n{1}", Filename, ex);
            }

            if (Configuration == null)
                Configuration = new TextMinerServiceSettings();
        }
    }
}
