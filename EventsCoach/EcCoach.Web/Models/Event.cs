using EcCoach.Web.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcCoach.Web.Models
{
    public class Event
    {

        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        public string CoachId { get; set; }
        public ApplicationUser Coach { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public int TypeId { get; set; }
        //[ForeignKey("TypeId")]
        public Type Type { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Venue { get; set; }

        public long Longitude { get; set; }

        public long Latitude { get; set; }
        
        public short? MaxCapacity { get; set; }

    }
}
