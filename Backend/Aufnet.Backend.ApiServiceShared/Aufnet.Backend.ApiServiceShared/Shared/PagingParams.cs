using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class PagingParams
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public bool IsValid => 
            Page >= 0 && ItemsPerPage >= 0 && ItemsPerPage <= 20; // <= 20 to stop funny things from happening
    }

    class CategoryPagingParams : PagingParams
    {
    }
}
