using EcCoach.Web.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcCoach.Web.Models
{
    public class Event
    {

        public int Id { get; set; }

        [Required]
        public ApplicationUser Coach { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public Type Type { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Venue { get; set; }

        public long Longitude { get; set; }

        public long Latitude { get; set; }

        public short? MaxCapacity { get; set; }

    }
}
