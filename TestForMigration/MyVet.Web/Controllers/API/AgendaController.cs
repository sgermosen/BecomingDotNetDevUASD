using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVet.Common.Models;
using MyVet.Web.Data;
using MyVet.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Controllers.API
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AgendaController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IConverterHelper _converterHelper;

        public AgendaController(
            DataContext dataContext,
            IConverterHelper converterHelper)
        {
            _dataContext = dataContext;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("GetAgendaForOwner")]
        public async Task<IActionResult> GetAgendaForOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agendas = await _dataContext.Agendas
                .Include(a => a.Owner)
                .ThenInclude(o => o.User)
                .Include(a => a.Pet)
                .ThenInclude(p => p.PetType)
                .Where(a => a.Date >= DateTime.Today.ToUniversalTime())
                .OrderBy(a => a.Date)
                .ToListAsync();

            var response = new List<AgendaResponse>();
            foreach (var agenda in agendas)
            {
                var agendaRespose = new AgendaResponse
                {
                    Date = agenda.Date,
                    Id = agenda.Id,
                    IsAvailable = agenda.IsAvailable
                };

                if (agenda.Owner != null)
                {
                    if (agenda.Owner.User.Email.ToLower().Equals(emailRequest.Email.ToLower()))
                    {
                        agendaRespose.Owner = _converterHelper.ToOwnerResposne(agenda.Owner);
                        agendaRespose.Pet = _converterHelper.ToPetResponse(agenda.Pet);
                        agendaRespose.Remarks = agenda.Remarks;
                    }
                    else
                    {
                        agendaRespose.Owner = new OwnerResponse { FirstName = "Reserved" };
                    }
                }

                response.Add(agendaRespose);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("AssignAgenda")]
        public async Task<IActionResult> AssignAgenda(AssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agenda = await _dataContext.Agendas.FindAsync(request.AgendaId);
            if (agenda == null)
            {
                return BadRequest("Agenda doesn't exists.");
            }

            if (!agenda.IsAvailable)
            {
                return BadRequest("Agenda is not available.");
            }

            var owner = await _dataContext.Owners.FindAsync(request.OwnerId);
            if (owner == null)
            {
                return BadRequest("Owner doesn't exists.");
            }

            var pet = await _dataContext.Pets.FindAsync(request.PetId);
            if (pet == null)
            {
                return BadRequest("Pet doesn't exists.");
            }

            agenda.IsAvailable = false;
            agenda.Remarks = request.Remarks;
            agenda.Owner = owner;
            agenda.Pet = pet;

            _dataContext.Agendas.Update(agenda);
            await _dataContext.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost]
        [Route("UnAssignAgenda")]
        public async Task<IActionResult> UnAssignAgenda(UnAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agenda = await _dataContext.Agendas
                .Include(a => a.Owner)
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(a => a.Id == request.AgendaId);
            if (agenda == null)
            {
                return BadRequest("Agenda doesn't exists.");
            }

            if (agenda.IsAvailable)
            {
                return BadRequest("Agenda is available.");
            }

            agenda.IsAvailable = true;
            agenda.Remarks = null;
            agenda.Owner = null;
            agenda.Pet = null;

            _dataContext.Agendas.Update(agenda);
            await _dataContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
