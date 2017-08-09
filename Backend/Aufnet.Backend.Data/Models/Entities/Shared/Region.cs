using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Region : Entity
    {
        public string Name { set; get; }
        public virtual Point Center { get; set; }
    }
}
