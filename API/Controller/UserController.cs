using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.User;
using API.Helpers;
using API.Interface;
using API.Domain;
using API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Domain.Models;

namespace API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        [HttpGet("get/users")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginationResponse<UserDetailsDTO>>> GetUsers([FromQuery] UserParams userParams)
        {
            var users = await userRepository.GetAsync(userParams);

            var response = new PaginationResponse<UserDetailsDTO>
            {
                Items = users,
                TotalCount = users.Count
            };

            return Ok(response);
        }

        [HttpGet("get/users/{id:int}")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppUser>> GetUser([FromRoute] int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null) return NotFound("User Not Found!");

            return Ok(user);
        }

        [HttpDelete("delete/users")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppUser>> DeleteUser([FromRoute] int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null) return NotFound("User Not Found!");

            await userRepository.DeleteAsync(user);

            await userRepository.SaveChangesAsync();

            return Ok(user);
        }
    }
}