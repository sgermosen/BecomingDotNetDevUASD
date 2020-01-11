using EcCoach.Web.Data;
using EcCoach.Web.Dtos;
using EcCoach.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcCoach.Web.Controllers.API
{
    [Route("api/[Controller]")]
    [Authorize]
    public class AttendancesController:Controller
    {

        private readonly DataContext _context;

        public AttendancesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Attend(AttendanceDto dto )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.EventId == dto.EventId);
            if (exists)
                return BadRequest("The attendance allready exists");

            var attendance = new Attendance
            {
                EventId = dto.EventId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IActionResult DeleteAttendance(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var attendance = _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.EventId == id);
            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return Ok();

        }

    }
}
