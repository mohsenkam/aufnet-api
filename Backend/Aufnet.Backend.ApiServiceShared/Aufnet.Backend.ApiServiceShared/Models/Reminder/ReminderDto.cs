using System;
using Aufnet.Backend.Data.Models.Entities.Identity;

namespace Aufnet.Backend.ApiServiceShared.Models.Reminder
{
    public class ReminderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TrigerDate { get; set; }
        public DateTime TrigerTime { get; set; }

    }
}