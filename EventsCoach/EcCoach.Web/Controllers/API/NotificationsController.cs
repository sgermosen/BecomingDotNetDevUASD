using AutoMapper;
using EcCoach.Web.Data;
using EcCoach.Web.Dtos;
using EcCoach.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;

namespace EcCoach.Web.Controllers.API
{
    //  [Route("api/[controller]")]
    // [ApiController]
    public class NotificationsController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public NotificationsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = "1e8d2f2f-b3c8-4acd-abfe-0da932a0a551";// User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notifications = _context.UserNotifications
            .Where(un => un.UserId == userId && !un.IsRead)
            .Select(un => un.Notification)
            .Include(n => n.Event.Coach)
            .ToList();

            //Forma 1 de iterar las notificaciones para convertirlas en el nuevo objeto
            // var notificationList = new Collection<NotificationDto>(); //Creamos un objeto de tipo lista de DTO porque es lo que vamos a retornar
            //foreach (var item in notifications)
            //{
            //    var tempNotifDto = new NotificationDto
            //    {
            //        DateTime = item.DateTime,
            //        OriginalDateTime = item.OriginalDateTime,
            //        OriginalVenue = item.OriginalVenue,
            //        NotificationType = item.NotificationType,
            //        Event = new EventDto
            //        {
            //            DateTime = item.Event.DateTime,
            //            Id = item.Event.Id,
            //            IsCanceled = item.Event.IsCanceled,
            //            Venue = item.Event.Venue,
            //            Coach = new UserDto
            //            {
            //                Id = item.Event.Coach.Id,
            //                Name = item.Event.Coach.Name
            //            }
            //        }
            //    };  //Creamos el objeto que vamos a almacenar en la coleccion

            //    //tempEventDto.Coach = tempUserDto; //llenamos el coach del evento
            //    //tempNotifDto.Event = tempEventDto; //llenamos el evento de la notificacion
            //    notificationList.Add(tempNotifDto);  //Agregamos a la coleccion que vamos a devolver el objeto creado
            //}
            //return notificationList;

            //Forma 2 de iterar de forma directa usando LINQ
            //return notifications.Select(n => new NotificationDto()
            //{
            //    DateTime = n.DateTime,
            //    OriginalDateTime = n.OriginalDateTime,
            //    OriginalVenue = n.OriginalVenue,
            //    NotificationType = n.NotificationType
            //    Event = new EventDto
            //    {
            //        Coach = new UserDto
            //        {
            //            Id = n.Event.Coach.Id,
            //            Name = n.Event.Coach.Name
            //        },
            //        DateTime = n.Event.DateTime,
            //        Id = n.Event.Id,
            //        IsCanceled = n.Event.IsCanceled,
            //        Venue = n.Event.Venue,
            //    },
            //});

           

            return notifications.Select(_mapper.Map<Notification, NotificationDto>);


        }
    }
}