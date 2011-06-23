using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextMiner.Properties.Settings
{
    public partial class TextMinerServiceSettingsMiner
    {
        public TextMinerServiceSettingsMiner()
        {
            this.Enabled = false;
            this.Name = MinerName.Unsupported;
            this.Uri = string.Empty;
            this.Arguments = string.Empty;
            this.ResponseTimeout = 30000;
            this.RetriesOnError = 5;
            this.IntervalOnRetry = 5000;
        }
    }
}
