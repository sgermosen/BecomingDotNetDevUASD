using EcCoach.Web.Data;
using EcCoach.Web.Dtos;
using EcCoach.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EcCoach.Web.Controllers.API
{
  //  [Route("api/[controller]")]
    // [ApiController]
    public class NotificationsController : Controller
    {
        private readonly DataContext _context;

        public NotificationsController(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notifications = _context.UserNotifications
            .Where(un => un.UserId == userId && !un.IsRead)
            .Select(un => un.Notification)
            .Include(n => n.Event.Coach)
            .ToList();

            return notifications.Select(n => new NotificationDto()
            {
                DateTime = n.DateTime,
                Event = new EventDto
                {
                    Coach = new UserDto
                    {
                        Id = n.Event.Coach.Id,
                        Name = n.Event.Coach.Name
                    },
                    DateTime = n.Event.DateTime,
                    Id = n.Event.Id,
                    IsCanceled = n.Event.IsCanceled,
                    Venue = n.Event.Venue,
                },
                OriginalDateTime = n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                NotificationType = n.NotificationType
            });

            
        }
    }
}