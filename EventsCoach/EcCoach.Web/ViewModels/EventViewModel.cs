using EcCoach.Web.Data;
using EcCoach.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcCoach.Web.ViewModels
{
    public class EventViewModel  
    {



        //[Required]
        public int TypeId { get; set; }
        //public Type Type { get; set; }

        [Required]
         [StringLength(100, MinimumLength = 5)]
        public string Venue { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public DateTime FullDate
        {
            get 
                {
                return DateTime.Parse($"{Date} {Time}");
                }
        }

        //public long Longitude { get; set; }

        //public long Latitude { get; set; }

        //public short? MaxCapacity { get; set; }
    }
}
