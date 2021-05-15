using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IncidentDto>))]
        public IActionResult Get()
        {
            return Ok(_incidentService.GetAll());
        }


        [HttpGet("unassigned")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IncidentDto>))]
        public IActionResult GetUnassignedIcidents()
        {
            return Ok(_incidentService.GetUnassignedIncidents());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IncidentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            try
            {
                IncidentDto Incident = _incidentService.Get(id);

                return Ok(Incident);
            }
            catch (IncidentNotFoundException incidentNotFoud)
            {
                return NotFound(incidentNotFoud.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IncidentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateIncident([FromBody] IncidentDto incident)
        {
            try
            {
                IncidentDto newIncident = _incidentService.Insert(incident);

                return CreatedAtAction(nameof(GetById), new { id = newIncident.ID }, newIncident);
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }        
            catch (InvalidIncidentException invalidIncident)
            {
                return BadRequest(invalidIncident.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IncidentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateIncident(int id, [FromBody] IncidentDto incident)
        {
            if (id != incident.ID)
                return BadRequest($"Request id and entity id don't match");
            try
            {
                IncidentDto modified = _incidentService.Update(incident);
                return Ok(modified);
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (InvalidIncidentException invalidIncident)
            {
                return BadRequest(invalidIncident.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteIncident(int id)
        {
            try
            {
                _incidentService.Delete(id);

                return NoContent();
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }
        }



        [HttpPut("incident/{incidentId}/crew/{crewId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IncidentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public IActionResult AddCrewToIncident(int incidentId, int crewId)
        {
         
            try
            {

                IncidentDto modified = _incidentService.AddCrewToIncident(incidentId, crewId);

                return Ok(modified);
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }        
            catch (CrewNotFoundException crewNotFound)
            {
                return NotFound(crewNotFound.Message);
            }
        }



        [HttpPut("remove-crew/incident/{incidentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IncidentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveCrewFromIncidet(int incidentId)
        {

            try
            {

                IncidentDto updatedIncindet = _incidentService.RemoveCrewFromIncidet(incidentId);

                return Ok(updatedIncindet);
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }
            catch (CrewNotFoundException crewNotFound)
            {
                return NotFound(crewNotFound.Message);
            }
            catch (InvalidDeviceUsageException deviceUsageExcpetion)
            {
                return BadRequest(deviceUsageExcpetion.Message);
            }
        }


        [HttpPost("incident/{incidentId}/device/{deviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddDeviceToIncident(int incidentId, int deviceId)
        {
            try
            {
                _incidentService.AddDeviceToIncident(incidentId, deviceId);
               
                return Ok();
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }
            catch (InvalidIncidentException invalidIncident)
            {
                return BadRequest(invalidIncident.Message);
            }
            catch (DeviceNotFoundException deviceNotFound)
            {
                return NotFound(deviceNotFound.Message);
            }
            catch (InvalidDeviceUsageException invalidDeviceUsage)
            {
                return NotFound(invalidDeviceUsage.Message);
            }


            
        }


        [HttpPut("remove-device/incident/{incidentId}/device/{deviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RemoveDeviceFromIncindet(int incidentId, int deviceId)
        {
            try
            {
                _incidentService.RemoveDeviceFromIncindet(incidentId, deviceId);

                return Ok();
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }          
            catch (DeviceNotFoundException deviceNotFound)
            {
                return NotFound(deviceNotFound.Message);
            }
            catch (InvalidDeviceUsageException invalidDeviceUsage)
            {
                return NotFound(invalidDeviceUsage.Message);
            }



        }






    }
}
