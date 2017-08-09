using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.Data.Models.Entities.Shared
{
    public class Point : Entity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
