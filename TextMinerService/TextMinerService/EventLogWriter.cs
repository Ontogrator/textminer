using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TextMiner
{
    static class EventLogWriter
    {
        private static string _source = "Text Miner Service";
        private static string _log = "Application";

        static EventLogWriter()
        {
            if (!EventLog.SourceExists(_source))
                EventLog.CreateEventSource(_source, _log);
        }

        public static void WriteWarning(string s, params object[] args)
        {
            EventLog.WriteEntry(_source, string.Format(s, args), EventLogEntryType.Warning);
        }

        public static void WriteError(string s, params object[] args)
        {
            EventLog.WriteEntry(_source, string.Format(s, args), EventLogEntryType.Error);
        }

        public static void WriteInformation(string s, params object[] args)
        {
            EventLog.WriteEntry(_source, string.Format(s, args), EventLogEntryType.Information);
        }
    }
}
