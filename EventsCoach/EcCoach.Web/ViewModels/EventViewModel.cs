using EcCoach.Web.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcCoach.Web.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Venue { get; set; }

        [FutureDate]
        public string Date { get; set; }

        public string Time { get; set; }

        public string Header
        {
            get { return (Id != 0) ? "Updating Event" : "Creating Event"; }
        }

        public string Action
        {
            get { return (Id != 0) ? "Update" : "Create"; }
        }
   

        public DateTime GetFullDate()
        {
                return DateTime.Parse($"{Date} {Time}");            
        }

        //public long Longitude { get; set; }

        //public long Latitude { get; set; }

        //public short? MaxCapacity { get; set; }
    }
}
