using System;
using System.Numerics;
using Aufnet.Backend.Data.Models.Entities.Merchant;

namespace Aufnet.Backend.ApiServiceShared.Models.Reminder
{
    public class ReminderDto
    {
        public int Id { get; set; }
        public DateTime TrigerDateTime { get; set; }
        public MerchantEvent Event { get; set; }
    }
}
