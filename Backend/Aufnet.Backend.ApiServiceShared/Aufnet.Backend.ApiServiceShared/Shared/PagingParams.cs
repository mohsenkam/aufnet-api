using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class PagingParams
    {
        public int Offset { get; set; }
        public int Count { get; set; }

        public bool IsValid => 
            Offset >= 0 && Count >= 0;
    }
}
