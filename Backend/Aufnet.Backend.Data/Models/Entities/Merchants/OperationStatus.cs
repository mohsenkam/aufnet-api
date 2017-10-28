using System;
using System.ComponentModel.DataAnnotations;

namespace Aufnet.Backend.Data.Models.Entities.Merchants
{
    public class OperationStatus: Entity
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartHour { get; set; }

        [DataType(DataType.Time)]
        public DateTime FinishHour { get; set; }
    }
}