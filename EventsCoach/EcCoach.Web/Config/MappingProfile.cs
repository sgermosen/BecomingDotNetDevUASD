using AutoMapper;
using EcCoach.Web.Data;
using EcCoach.Web.Dtos;
using EcCoach.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcCoach.Web.Config
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });

             config.CreateMapper();
        }
    }
}
