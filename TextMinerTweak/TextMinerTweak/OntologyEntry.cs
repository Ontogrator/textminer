using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextMinerTweak
{
    class OntologyEntry
    {
        public string ID;
        public string AltID;
        public string Label;
        public string Definition = string.Empty;
        public List<string> Parents = new List<string>();
        public List<int[]> LeftRights = new List<int[]>();
        public List<OntologyEntry> Children = new List<OntologyEntry>();
        public List<string> Synonyms = new List<string>();

        public OntologyEntry(string id, string label, string altID)
        {
            ID = id;
            Label = label;
            AltID = altID;
        }
    }
}
