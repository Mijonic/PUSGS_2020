using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartEnergyAPI.Controllers
{
    [Route("api/work-requests")]
    [ApiController]
    public class WorkRequestController : ControllerBase
    {
        private readonly IWorkRequestService _workRequestService;

        public WorkRequestController(IWorkRequestService workRequestService)
        {
            _workRequestService = workRequestService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkRequestDto>))]
        public IActionResult Get()
        {
            return Ok(_workRequestService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            try
            {
                WorkRequestDto workRequest = _workRequestService.Get(id);
                return Ok(workRequest);
            }catch(WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateWorkRequest([FromBody] WorkRequestDto workRequest)
        {
            try
            {
                WorkRequestDto newWorkRequest = _workRequestService.Insert(workRequest);
                return CreatedAtAction(nameof(GetById), new { id = newWorkRequest.ID}, newWorkRequest);
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }catch(WorkRequestInvalidStateException wris)
            {
                return BadRequest(wris.Message);
            }
        }


    }
}
