using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergyAPI.Controllers
{
    [Route("api/incidents")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet("{id}/location")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationDto))]
        public IActionResult GetIncidentLocation(int id)
        {
            try
            {
                return Ok(_incidentService.GetIncidentLocation(id));
            }catch(IncidentNotFoundException inf)
            {
                return BadRequest(inf.Message);
            }catch(LocationNotFoundException lnf)
            {
                return NotFound(lnf.Message);
            }
        }
    }
}
