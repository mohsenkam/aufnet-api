using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.ApiServiceShared.Shared.utils
{
    public static class UtilityMethods
    {
        private static readonly string[] _mappings = { "q", "F", "E", "P", "C", "G", "O", "a", "Z", "R" };

        public static string GenerateTrackingId(DateTime dateTime)
        {
            
            var now = dateTime.ToString("MMddHHmmssfff");

            var trackingId = "";
            foreach (var c in now)
            {
                trackingId += _mappings[(int) Char.GetNumericValue(c)];
            }
            return trackingId;
        }

    }
}
