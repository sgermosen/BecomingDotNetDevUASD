using EcCoach.Web.Controllers;
using EcCoach.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace EcCoach.Web.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public byte Type { get; set; }

        public IEnumerable<Type> Types { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Venue { get; set; }

        [FutureDate]
        public string Date { get; set; }

        [ValidTime]
        public string Time { get; set; }

        public string Header
        {
            get { return (Id != 0) ? "Updating Event" : "Creating Event"; }
        }

        public string Heading { get; set; }

        public string Action
        {
            get {
                Expression<Func<EventsController, IActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<EventsController, IActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        //public string Action
        //{
        //    get { return (Id != 0) ? "Update" : "Create"; }
        //}

       
        public DateTime GetFullDate()
        {
                return DateTime.Parse($"{Date} {Time}");            
        }

        //public long Longitude { get; set; }

        //public long Latitude { get; set; }

        //public short? MaxCapacity { get; set; }
    }
}
