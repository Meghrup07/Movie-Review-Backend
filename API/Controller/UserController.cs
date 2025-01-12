using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Domain.Models;
using API.Services.Interfaces;
using API.Comman;
using API.Contracts.User;

namespace API.Controller
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/user")]
    public class UserController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("list")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginationResponse<UserDetailsDTO>>> GetUsers([FromQuery] UserParams userParams)
        {
            try
            {
                var users = await serviceManager.UserService.GetAsync(userParams);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppUser>> GetUser([FromRoute] int id)
        {
            try
            {
                var user = await serviceManager.UserService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppUser>> DeleteUser([FromRoute] int id)
        {
            try
            {
                var user = await serviceManager.UserService.DeleteAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex);

            }
        }
    }
}