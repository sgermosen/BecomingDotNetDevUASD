using EcCoach.Web.Data;
using EcCoach.Web.Helpers;
using EcCoach.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EcCoach.Web.Models
{
    public class Event : AuditEntity, ISoftDeleted
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

        public bool IsCanceled { get; private set; }

        public ICollection<Attendance> Attendances { get;  set; }
        
        public Event()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;
              var notification = Notification.EventCanceled(this);
            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, int typeId)
        {
            var notification = Notification.EventUpdated(this,DateTime, Venue);
        
            DateTime = dateTime;
            TypeId = typeId;
            Venue = venue;
            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        // public ICollection<Attendance> Attendances { get; set; }

    }
}
