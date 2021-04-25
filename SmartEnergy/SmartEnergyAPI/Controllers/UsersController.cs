using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.User;
using SmartEnergy.Contract.DTO;
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

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());

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


    }
}
