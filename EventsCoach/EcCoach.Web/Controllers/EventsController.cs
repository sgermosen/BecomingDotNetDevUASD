using EcCoach.Web.Data;
using EcCoach.Web.Models;
using EcCoach.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Authorize]
        // [HttpGet]
        public IActionResult Create()
        {
            var types = _context.Types.ToList();

            ViewBag.TypeList = new SelectList(types, "Id", "Name");
            //this is a comment 

            return View();
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
                    DateTime = vm.FullDate,
                    Latitude = 0,
                    Longitude = 0,
                    TypeId = vm.TypeId,
                    MaxCapacity = 0

                    //Type= type
                };

                //  _context.Events.Add(ev);
                _context.Add(model);

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            var types = _context.Types.ToList();
            ViewBag.TypeList = new SelectList(types, "Id", "Name", vm.TypeId);

            ViewBag.Msg = "erro on Model";
            return View();


        }
    }
}