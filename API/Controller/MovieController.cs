using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Contracts.Movie;
using API.Comman;
using API.Services.Interfaces;
using API.Domain;

namespace API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/movie")]
    public class MovieController(IServiceManager serviceManager, DataContext context) : ControllerBase
    {
        [HttpGet("list")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginationResponse<MovieDTO>>> GetMovies([FromQuery] UserParams userParams)
        {

            try
            {
                var movies = await serviceManager.MovieService.GetAsync(userParams);

                return Ok(movies);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetMovie([FromRoute] int id)
        {

            try
            {
                var movie = await serviceManager.MovieService.GetByIdAsync(id);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpPost("add")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movie>> CreateMovie(AddMovieDTO addMovieDTO)
        {

            if (await MovieNameExits(addMovieDTO.MovieName)) return BadRequest("Movie already exits!");

            try
            {
                var movie = await serviceManager.MovieService.CreateAsync(addMovieDTO);

                if (addMovieDTO.MoviePic != null)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "images");

                    // Check if the directory exists, and create it if it doesn't
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + addMovieDTO.MoviePic.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await addMovieDTO.MoviePic.CopyToAsync(stream);
                    }

                    movie.MoviePic = $"{Request.Scheme}://{Request.Host}/images/{uniqueFileName}";

                }

                await serviceManager.MovieService.UpdateAsync(movie.Id, new UpdateMovieDTO
                {
                    MovieName = movie.MovieName,
                    DirectorName = movie.DirectorName,
                    ReleaseYear = movie.ReleaseYear,
                    MoviePic = null
                });

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> UpdateMovie([FromRoute] int id, UpdateMovieDTO updateMovieDTO)
        {
            try
            {
                var movie = await serviceManager.MovieService.GetByIdAsync(id);

                if (updateMovieDTO.MoviePic != null)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "images");

                    // Check if the directory exists, and create it if it doesn't
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + updateMovieDTO.MoviePic.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the new profile picture
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateMovieDTO.MoviePic.CopyToAsync(stream);
                    }

                    // Update the user's ProfilePic property with the new image URL
                    movie.MoviePic = $"{Request.Scheme}://{Request.Host}/images/{uniqueFileName}";
                }

                movie.MovieName = updateMovieDTO.MovieName;
                movie.DirectorName = updateMovieDTO.DirectorName;
                movie.ReleaseYear = updateMovieDTO.ReleaseYear;

                await serviceManager.MovieService.UpdateAsync(movie.Id, updateMovieDTO);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> DeleteMovie([FromRoute] int id)
        {

            try
            {
                var movie = await serviceManager.MovieService.DeleteAsync(id);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        private async Task<bool> MovieNameExits(string movieName)
        {
            return await context.Movies.AnyAsync(m => m.MovieName == movieName);
        }
    }
}