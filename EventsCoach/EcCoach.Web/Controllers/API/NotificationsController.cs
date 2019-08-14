using AutoMapper;
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
            var userId = "1e8d2f2f-b3c8-4acd-abfe-0da932a0a551";// User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notifications = _context.UserNotifications
            .Where(un => un.UserId == userId && !un.IsRead)
            .Select(un => un.Notification)
            .Include(n => n.Event.Coach)
            .ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });

            IMapper iMapper = config.CreateMapper();

            return notifications.Select(iMapper.Map<Notification, NotificationDto>);


        }
    }
}