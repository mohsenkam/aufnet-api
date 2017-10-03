using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class SearchParams
    {
        public string Suburb { get; set; }
        public int? Distance { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }

        public bool IsValid => 
            Offset >= 0 && Count >= 0;
    }
}
