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
        public IActionResult GetUnassignedIncidents()
        {
            return Ok(_incidentService.GetUnassignedIncidents());
        }

        [HttpGet("unresolved")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IncidentMapDisplayDto>))]
        public IActionResult GetUnresolvedIncidents()
        {
            return Ok(_incidentService.GetUnresolvedIncidentsForMap());
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



        [HttpPut("{incidentId}/crew/{crewId}")]
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



        [HttpPut("{incidentId}/remove-crew")]
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

        
        [HttpPost("{incidentId}/device/{deviceId}")]
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


        [HttpPut("{incidentId}/remove-device/device/{deviceId}")]
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



        [HttpGet("{incidentId}/calls")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IncidentDto>))]
        public IActionResult GetIncidentCalls(int incidentId)
        {
            return Ok(_incidentService.GetIncidentCalls(incidentId));
        }


        [HttpGet("{incidentId}/calls-counter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public IActionResult GetNumberOfIncidentCalls(int incidentId)
        {
            return Ok(_incidentService.GetNumberOfCalls(incidentId));
        }

        [HttpGet("{incidentId}/affected-consumers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public IActionResult GetNumberOfAffectedConsumers(int incidentId)
        {
            try
            {
                return Ok(_incidentService.GetNumberOfAffectedConsumers(incidentId));
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }
        }


        [HttpGet("{incidentId}/devices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        public IActionResult GetIncidentDevices(int incidentId)
        {
            try
            {
                return Ok(_incidentService.GetIncidentDevices(incidentId));
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }
        }


        [HttpGet("{incidentId}/unrelated-devices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        public IActionResult GetUnrelatedDevices(int incidentId)
        {
            try
            {
                return Ok(_incidentService.GetUnrelatedDevices(incidentId));
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }
        }





        [HttpPut("{incidentId}/set-priority")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SetIncidentPriority(int incidentId)
        {

            try
            {

                 _incidentService.RemoveCrewFromIncidet(incidentId);

                return Ok("Updated incident priority.");
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }

           
        }


    }
}
