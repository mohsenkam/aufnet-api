using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.ApiServiceShared.Models.Shared
{
    public class RegionDto
    {
        public string Name { set; get; }
        public PointDto Center { get; set; }
    }
}
