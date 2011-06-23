using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextMiner
{
    class Hit
    {
        public string OntologyID;
        public Dictionary<string, List<string>> Keywords;

        public Hit(string ontologyID)
        {
            OntologyID = ontologyID;
            Keywords = new Dictionary<string, List<string>>();
        }
    }
}
