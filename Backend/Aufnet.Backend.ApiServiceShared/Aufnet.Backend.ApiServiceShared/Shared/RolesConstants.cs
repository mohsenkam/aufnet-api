using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public static class RolesConstants
    {
        public const string Customer = "customer";
        public const string Merchant = "merchant";
        public const string TicketAttedant = "ticket_attendant";
        public const string Manager = "manager";
        public static readonly string[] Roles = {Customer, Merchant, TicketAttedant, Manager};
    }
}
