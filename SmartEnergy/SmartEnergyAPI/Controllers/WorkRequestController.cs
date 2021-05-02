using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Multimedia;
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
        private readonly IMultimediaService _multimediaService;

        public WorkRequestController(IWorkRequestService workRequestService, IMultimediaService multimediaService)
        {
            _workRequestService = workRequestService;
            _multimediaService = multimediaService;
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

        [HttpGet("{id}/devices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetWorkRequestDevices(int id)
        {
            try
            {
                return Ok(_workRequestService.GetWorkRequestDevices(id));
            }
            catch (WorkRequestNotFound wnf)
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
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (WorkRequestInvalidStateException wris)
            {
                return BadRequest(wris.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateWorkRequest(int id,[FromBody] WorkRequestDto workRequest)
        {
            if (id != workRequest.ID)
                return BadRequest($"Request id and entity id don't match");
            try
            {
                WorkRequestDto modified = _workRequestService.Update(workRequest);
                return Ok(modified);
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (WorkRequestInvalidStateException wris)
            {
                return BadRequest(wris.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteWorkRequest(int id)
        {
            try
            {
                _workRequestService.Delete(id);
                return NoContent();
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }

        [HttpPost("{id}/attachments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public IActionResult AttachFile(int id, IFormFile file)
        {
            try
            {
                _multimediaService.AttachFileToWorkRequest(file, id);
                return Ok();
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaAlreadyExists mae)
            {
                return BadRequest(mae.Message);
            }
        }

        [HttpGet("{id}/attachments/{filename}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AttachFile(int id, string filename)
        {
            try
            {
                return File(_multimediaService.GetWorkRequestAttachmentStream(id, filename), "application/octet-stream", filename);
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaNotFoundException mne)
            {
                return NotFound(mne.Message);
            }
        }


        [HttpGet("{id}/attachments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MultimediaAttachmentDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult  GetWorkRequestAttachments(int id)
        {
            try
            {
                return Ok(_multimediaService.GetWorkRequestAttachments(id));
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }

        [HttpDelete("{id}/attachments/{filename}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetWorkRequestAttachments(int id, string filename)
        {
            try
            {
                _multimediaService.DeleteWorkRequestAttachment(id, filename);
                return Ok();
            }
            catch (WorkRequestNotFound wnf)
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
