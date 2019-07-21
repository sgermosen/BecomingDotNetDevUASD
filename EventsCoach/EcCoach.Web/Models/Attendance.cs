using EcCoach.Web.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcCoach.Web.Models
{
    public class Attendance
    {
        //  public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int EventId { get; set; }

        public Event Event { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }

        public ApplicationUser Attendee { get; set; }
    }
}
