using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.Data.Models.Entities.Admin
{
    public class Category: Entity
    {
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}
