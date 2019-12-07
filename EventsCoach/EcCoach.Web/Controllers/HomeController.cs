using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcCoach.Web.Models;
using EcCoach.Web.Data;
using Microsoft.EntityFrameworkCore;
using EcCoach.Web.ViewModels;

namespace EcCoach.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Search(EventsViewModel vm)
        {
            return RedirectToAction("Index", new { query = vm.SearchTerm });
        }

        public IActionResult Index(string query = null)
        {
            var upcomingEvents = _context.Events
                                .Include(c => c.Coach)
                                .Include(c => c.Type)
                                .Where(p => p.DateTime > DateTime.Now && !p.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents.Where(g =>
                g.Coach.Name.Contains(query) ||
                g.Type.Name.Contains(query) ||
                g.Venue.Contains(query));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var attendances = _context.Attendances.Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a=> a.EventId);

;            var vm = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents.ToList(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Events ", //My upcoming events on the other screen SearchTerm = query
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Events", vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
