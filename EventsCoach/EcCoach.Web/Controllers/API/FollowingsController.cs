using EcCoach.Web.Data;
using EcCoach.Web.Dtos;
using EcCoach.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace EcCoach.Web.Controllers.API
{
    [Route("api/[Controller]")]
    [Authorize]
    public class FollowingsController : Controller
    {

        private readonly DataContext _context;

        public FollowingsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetFollowings()
        {
            return this.Ok(_context.Events.ToList());
        }

        [HttpPost]
        public ActionResult Follow(FollowingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exists = _context.Followings.Any(a => a.FollowerId == userId && a.FollowerId == dto.FolloweeId);
            if (exists)
                return BadRequest("The attendance allready exists");

            var follow = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            _context.Followings.Add(follow);
            _context.SaveChanges();
            return Ok();
        }

        // TODO: Provocar error para indicar el problema de tener una misma api para dos cosas
        //[HttpPost]
        //public async Task<IActionResult> Attend(AttendanceDto dto)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.EventId == dto.EventId);
        //    if (exists)
        //        return BadRequest("The attendance allready exists");

        //    var attendance = new Attendance
        //    {
        //        EventId = dto.EventId,
        //        AttendeeId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        //    };
        //    _context.Attendances.Add(attendance);
        //    _context.SaveChanges();
        //    return Ok();
        //}
    }
}
