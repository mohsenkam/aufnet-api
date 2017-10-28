using System;
using System.Numerics;
using Aufnet.Backend.Data.Models.Entities.Merchants;

namespace Aufnet.Backend.ApiServiceShared.Models.Reminder
{
    public class ReminderDto
    {
        public int Id { get; set; }
        public DateTime TrigerDateTime { get; set; }
        public ItemBasedOffer Event { get; set; }
    }
}
