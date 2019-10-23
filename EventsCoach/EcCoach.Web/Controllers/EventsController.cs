using EcCoach.Web.Data;
using EcCoach.Web.Models;
using EcCoach.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace EcCoach.Web.Controllers
{
    public class EventsController : Controller
    {
        public readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var ev = _context.Events
                             .Include(p => p.Attendances.Select(a => a.Attendee))
                             .Single(a => a.CoachId == userId && a.Id == id);

            if (ev.IsCanceled)
                return NotFound();

            ev.Cancel();

            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var events = _context.Events
            .Where(a => a.CoachId == userId && a.DateTime >= DateTime.Now && !a.IsCanceled)
            .Include(p => p.Type)
            .ToList();

            return View(events);

        }

        [Authorize]
        public ActionResult Following()
        {
            return View();
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var events = _context.Attendances
            .Where(a => a.AttendeeId == userId)
            .Select(a => a.Event)
            .Include(p => p.Type)
            .Include(p => p.Coach)
            .ToList();

            var vm = new EventsViewModel
            {
                UpcomingEvents = events,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I'm Attending"
            };

            return View("Events", vm);

        }

        public IActionResult Details(int id)
        {

            var ev = _context.Events
                .Include(p => p.Coach)
                .Include(p => p.Type)
                .SingleOrDefault(p => p.Id == id);

            if (ev == null)
                return NotFound();

            var vm = new EventDetailsViewModel { Event = ev };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                vm.IsAttending = _context.Attendances.Any(a => a.EventId == ev.Id && a.AttendeeId == userId);

                vm.IsFollowing = _context.Followings.Any(a => a.FolloweeId == ev.CoachId && a.FollowerId == userId);
            }

            return View(nameof(Details), vm);

        }

        //public IActionResult MyEvents()
        //{

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var upcomingEvents = _context.Events
        //        .Include(c => c.Coach)
        //        .Include(c => c.Type)
        //        .Where(p => p.DateTime >= DateTime.Now && p.CoachId == userId).ToList();

        //    var vm = new EventsViewModel
        //    {
        //        UpcomingEvents = upcomingEvents,
        //        ShowActions = User.Identity.IsAuthenticated,
        //        Heading = "My Events"
        //    };

        //    return View("Events", vm);
        //}

        [Authorize]
        // [HttpGet]
        public IActionResult Create()
        {
            var types = _context.Types.ToList();

            ViewBag.TypeList = new SelectList(types, "Id", "Name");
            //this is a comment 
            var vm = new EventViewModel
            {
                Date = DateTime.Now.ToString("dd/MM/yyyy"),

            };

            return View("EventForm", vm);
        }

        [HttpPost]
        public IActionResult Create(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // var user = _context.Users.Where(p => p.Id == userId).FirstOrDefault();
                //string.Format("{0} {1}", vm.Date, vm.Time);
                //var dd =  vm.Date + " " +  vm.Time;

                //var type = _context.Types.Where(p => p.Id == vm.TypeId).FirstOrDefault();
                var model = new Event
                {
                    CoachId = userId,
                    Venue = vm.Venue,
                    DateTime = vm.GetFullDate(),
                    Latitude = 0,
                    Longitude = 0,
                    TypeId = vm.TypeId,
                    MaxCapacity = 0

                    //Type= type
                };

                //  _context.Events.Add(ev);
                _context.Add(model);

                _context.SaveChanges();

                return RedirectToAction("Mine");

            }

            var types = _context.Types.ToList();
            ViewBag.TypeList = new SelectList(types, "Id", "Name", vm.TypeId);

            ViewBag.Msg = "erro on Model";
            return View("EventForm", vm);


        }


        [Authorize]
        // [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ev = _context.Events.Where(p => p.Id == id.Value && p.CoachId == userId).FirstOrDefault();

            if (ev == null)
                return NotFound();

            var types = _context.Types.ToList();

            ViewBag.TypeList = new SelectList(types, "Id", "Name", ev.TypeId);

            var vm = new EventViewModel
            {
                Id = ev.Id,
                TypeId = ev.TypeId,
                Date = ev.DateTime.ToString("dd/MM/yyyy"),
                Time = ev.DateTime.ToString("HH:mm"),
                Venue = ev.Venue,
            };
            //this is a comment 

            return View("EventForm", vm);
        }

        [HttpPost]
        public IActionResult Update(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var ev = _context.Events
                .Include(g => g.Attendances).ThenInclude(a => a.Attendee).Where
                (g => g.Id == vm.Id && g.CoachId == userId).Single();

                ev.Modify(vm.GetFullDate(), vm.Venue, vm.TypeId);
               // ev.Venue = vm.Venue;
                //ev.DateTime = vm.GetFullDate();
                //ev.Latitude = 0;
                //ev.Longitude = 0;
                //ev.TypeId = vm.TypeId;
                //ev.MaxCapacity = 0;

                _context.SaveChanges();

                return RedirectToAction("Mine");
            }

            var types = _context.Types.ToList();
            ViewBag.TypeList = new SelectList(types, "Id", "Name", vm.TypeId);

            ViewBag.Msg = "Error on Model";
            return View("EventForm", vm);


        }
    }
}