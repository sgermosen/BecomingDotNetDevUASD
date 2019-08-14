using EcCoach.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcCoach.Web.Dtos
{
    public class NotificationDto
    {
        

        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public EventDto Event { get; set; }
    }
}
