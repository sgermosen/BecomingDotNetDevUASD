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

        public string Date { get; set; }

        public string Time { get; set; }

        public DateTime FullDate
        {
            get {
                return DateTime.Parse($"{Date} {Time}");
            }
        }

        //public long Longitude { get; set; }

        //public long Latitude { get; set; }

        //public short? MaxCapacity { get; set; }
    }
}
