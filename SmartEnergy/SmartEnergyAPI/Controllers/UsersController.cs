using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Multimedia;
using SmartEnergy.Contract.CustomExceptions.User;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergyAPI.Controllers
{ 
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMultimediaService _multimediaService;

        public UsersController(IUserService userService, IMultimediaService multimediaService)
        {
            _userService = userService;
            _multimediaService = multimediaService;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CrewsListDto>))]
        public IActionResult GetAll([FromQuery] string searchParam, [FromQuery] UserField sortBy, [FromQuery] SortingDirection direction,
                                    [FromQuery][BindRequired] int page, [FromQuery][BindRequired] int perPage, [FromQuery] UserStatusFilter status,
                                    [FromQuery] UserTypeFilter type)
        {
            return Ok(_userService.GetUsersPaged(sortBy, direction, page, perPage, status, type, searchParam));

        }

        [HttpGet("{id}/avatar/{filename}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFile(int id, string filename)
        {
            try
            {
                return File(_multimediaService.GetUserAvatarStream(id, filename), "application/octet-stream", filename);
            }
            catch (UserNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaNotFoundException mne)
            {
                return NotFound(mne.Message);
            }
        }


        [HttpGet("unassigned-crew-members")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public IActionResult GetUnassignedCrewMembers()
        {
            return Ok(_userService.GetAllUnassignedCrewMembers());

        }


        [HttpPut("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ApproveUser(int id)
        {
            try
            {
                UserDto user = _userService.ApproveUser(id);
                return Ok(user);
            }catch(UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (UserInvalidStatusException ius)
            {
                return BadRequest(ius.Message);
            }

        }


        [HttpPut("{id}/deny")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DenyUser(int id)
        {
            try
            {
                UserDto user = _userService.DenyUser(id);
                return Ok(user);
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (UserInvalidStatusException ius)
            {
                return BadRequest(ius.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(int id)
        {
            try
            {
                UserDto user = _userService.Get(id);
                return Ok(user);
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateUser([FromBody] UserDto newUser)
        {
            try
            {
                UserDto user = _userService.Insert(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = user.ID}, user);
            }
            catch (CrewNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (InvalidUserDataException ius)
            {
                return BadRequest(ius.Message);
            }

        }

        [HttpPost("{id}/avatar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> AttachAvatarAsync(int id, IFormFile file)
        {
            try
            {
                await _multimediaService.AttachUserAvatar(file, id);
                return Ok();
            }
            catch (UserNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaInfectedException mie)
            {
                return BadRequest(mie.Message);
            }
            catch (MultimediaNotImageException mni)
            {
                return BadRequest(mni.Message);
            }
        }


    }
}
