using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Services.Interfaces;
using API.Contracts.MovieReview;

namespace API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/movie-review")]
    public class MovieReviewController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("list")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieReview>> GetMovieReviews()
        {
            try
            {
                var movieReview = await serviceManager.MovieReviewService.GetAsync();
                return Ok(movieReview);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReview>> GetMovieReview([FromRoute] int id)
        {
            try
            {
                var movieReview = await serviceManager.MovieReviewService.GetByIdAsync(id);
                return Ok(movieReview);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }


        [HttpPost("add")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieReview>> AddMovieReview([FromBody] AddMovieReviewDTO addMovieReviewDTO)
        {

            try
            {
                var movieReview = await serviceManager.MovieReviewService.CreateAsync(addMovieReviewDTO);
                return Ok(movieReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut("update/{id:int}")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReview>> UpdateMovie([FromRoute] int id, UpdateMovieReviewDTO updateMovieReviewDTO)
        {
            try
            {
                var movieReview = await serviceManager.MovieReviewService.UpdateAsync(id, updateMovieReviewDTO);
                return Ok(movieReview);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReview>> DeleteMovieReview([FromRoute] int id)
        {
            try
            {
                var movieReview = await serviceManager.MovieReviewService.DeleteAsync(id);
                return Ok(movieReview);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

    }
}