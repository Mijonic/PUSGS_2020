using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Call;
using SmartEnergy.Contract.CustomExceptions.Consumer;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.CustomExceptions.Multimedia;
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
        private readonly IMultimediaService _multimediaService;

        public IncidentController(IIncidentService incidentService, IMultimediaService multimediaService)
        {
            _incidentService = incidentService;
            _multimediaService = multimediaService;
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



        [HttpPut("{incidentId}/assign/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AssignIncidetToUser(int incidentId, int userId)
        {

            try
            {

                _incidentService.AssignIncidetToUser(incidentId, userId);

                return Ok();
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
            }
            catch (UserNotFoundException usernf)
            {
                return NotFound(usernf.Message);
            }
        }

        

        [HttpGet("{incidentId}/crew")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        public IActionResult GetIncidentCrew(int incidentId)
        {
            try
            {
                return Ok(_incidentService.GetIncidentCrew(incidentId));
            }
            catch (IncidentNotFoundException incidentNotFound)
            {
                return NotFound(incidentNotFound.Message);
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
            catch(InvalidDeviceException invalidDevice)
            {
                return BadRequest(invalidDevice.Message);
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


        [HttpPost("{incidentId}/calls")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CallDto))]
       // [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddIncidentCall(int incidentId, CallDto newCall)
        {
            try
            {
                _incidentService.AddIncidentCall(incidentId, newCall);

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
            catch (InvalidCallException invalidCall)
            {
                return BadRequest(invalidCall.Message);
            }
            catch (LocationNotFoundException notFound)
            {
                return NotFound(notFound.Message);
            }
            catch (ConsumerNotFoundException notFound)
            {
                return NotFound(notFound.Message);
            }





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


        [HttpPost("{id}/attachments")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> AttachFileAsync(int id, IFormFile file)
        {
            try
            {
                await _multimediaService.AttachFileToIncidentAsync(file, id);
                return Ok();
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaAlreadyExists mae)
            {
                return BadRequest(mae.Message);
            }
            catch (MultimediaInfectedException mie)
            {
                return BadRequest(mie.Message);
            }
        }

        [HttpGet("{id}/attachments/{filename}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFile(int id, string filename)
        {
            try
            {
                return File(_multimediaService.GetIncidentAttachmentStream(id, filename), "application/octet-stream", filename);
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaNotFoundException mne)
            {
                return NotFound(mne.Message);
            }
        }


        [HttpGet("{id}/attachments")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MultimediaAttachmentDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetIncidentAttachments(int id)
        {
            try
            {
                return Ok(_multimediaService.GetIncidentAttachments(id));
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
        }


        [HttpDelete("{id}/attachments/{filename}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAttachment(int id, string filename)
        {
            try
            {
                _multimediaService.DeleteIncidentAttachment(id, filename);
                return Ok();
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaNotFoundException mnf)
            {
                return NotFound(mnf.Message);
            }

        }


    }
}
