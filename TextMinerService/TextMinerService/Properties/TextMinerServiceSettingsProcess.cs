using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TextMiner.Properties.Settings
{
    public partial class TextMinerServiceSettingsProcess
    {
        internal Worker.Common Worker;
        internal Thread Thread;

        public TextMinerServiceSettingsProcess()
        {
            Type = ProcessType.Unassigned;
            Worker = null;
            Thread = null;
            Enabled = false;
            Timeout = 60000;
            PollingInterval = 5000;
            PostPollingInterval = 1000;
            ResponseTimeout = 30000;
            OntogratorTab = 0;
        }
    }
}
