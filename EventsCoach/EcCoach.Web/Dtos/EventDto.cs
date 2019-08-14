using System;

namespace EcCoach.Web.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Coach { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public TypeDto Type { get; set; }
    }
}
