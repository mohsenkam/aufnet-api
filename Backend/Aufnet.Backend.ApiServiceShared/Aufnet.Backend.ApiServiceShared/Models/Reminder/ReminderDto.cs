using System;
using Aufnet.Backend.Data.Models.Entities.Identity;

namespace Aufnet.Backend.ApiServiceShared.Models.Reminder
{
    public class ReminderDto
    {
        public int Id { get; set; }
        public DateTime TrigerDateTime { get; set; }

    }
}