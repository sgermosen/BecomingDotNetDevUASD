using EcCoach.Web.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcCoach.Web.ViewModels
{
    public class EventViewModel
    {
        [Required]
        public int TypeId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Venue { get; set; }

        [FutureDate]
        public string Date { get; set; }

        public string Time { get; set; }

        public DateTime GetFullDate()
        {
                return DateTime.Parse($"{Date} {Time}");            
        }

        //public long Longitude { get; set; }

        //public long Latitude { get; set; }

        //public short? MaxCapacity { get; set; }
    }
}
