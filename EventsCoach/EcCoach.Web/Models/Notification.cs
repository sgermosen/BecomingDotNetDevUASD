using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EcCoach.Web.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        [Required] public Event Event { get; private set; }

        protected Notification()
        {
            UserNotifications = new Collection<UserNotification>();
        }

        private Notification(NotificationType notificationType, Event ev)
        {
            Event = ev ?? throw new ArgumentNullException(nameof(ev));
            NotificationType = notificationType;
            DateTime = DateTime.Now;
        }

        public static Notification EventCreted(Event ev)
        {
            return new Notification(NotificationType.EventCreated, ev);
        }
        public static Notification EventUpdated(Event newEvent, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.EventUpdated, newEvent);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;
            return notification;
        }
        public static Notification EventCanceled(Event ev)
        {
            return new Notification(NotificationType.EventCanceled, ev);
        }

    }


}
