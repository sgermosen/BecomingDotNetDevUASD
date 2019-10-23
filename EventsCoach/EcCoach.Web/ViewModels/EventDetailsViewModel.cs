using EcCoach.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcCoach.Web.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }

        public bool IsAttending { get; set; }

        public bool IsFollowing { get; set; }
    }
}
