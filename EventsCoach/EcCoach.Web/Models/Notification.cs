using System;
using System.ComponentModel.DataAnnotations;

namespace EcCoach.Web.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        [Required] public Event Event { get; private set; }

        protected Notification()
        {
        }
        public Notification(NotificationType notificationType, Event ev)
        {
            Event = ev ?? throw new ArgumentNullException(nameof(ev));
            NotificationType = notificationType;
            DateTime = DateTime.Now;
        }
    }


}
