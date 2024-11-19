using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Movie;
using API.Helpers;
using API.Interface;
using API.Models;
using API.Response;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController(DataContext context, IMovieRepository movieRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginationResponse<MovieDTO>>> GetMovies([FromQuery] UserParams userParams)
        {

            var movies = await movieRepository.GetAsync(userParams);

            var response = new PaginationResponse<MovieDTO>
            {
                Items = movies,
                TotalCount = movies.Count
            };

            return Ok(response);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetMovie([FromRoute] int id)
        {

            var movie = await movieRepository.GetByIdAsync(id);

            if (movie == null) return NotFound("Movie Not Found!");

            return Ok(movie);

        }


        [HttpPost]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movie>> CreateMovie(AddMovieDTO addMovieDTO)
        {

            if (await MovieNameExits(addMovieDTO.MovieName)) return BadRequest("Movie already exits!");

            var movie = mapper.Map<Movie>(addMovieDTO);

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

            await movieRepository.CreateAsync(movie);

            await movieRepository.SaveChangesAsync();

            return Ok(movie);

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> UpdateMovie([FromRoute] int id, UpdateMovieDTO updateMovieDTO)
        {
            var movie = await movieRepository.GetByIdAsync(id);

            if (movie == null) return NotFound("Movie Not Found!");

            mapper.Map(updateMovieDTO, movie);

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

            await movieRepository.UpdateAsync(movie);

            await movieRepository.SaveChangesAsync();

            return Ok(movie);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> DeleteMovie([FromRoute] int id)
        {

            var movie = await movieRepository.GetByIdAsync(id);

            if (movie == null) return NotFound("Movie Not Found!");

            await movieRepository.DeleteAsync(movie);

            await movieRepository.SaveChangesAsync();

            return Ok(movie);

        }

        private async Task<bool> MovieNameExits(string movieName)
        {
            return await context.movies.AnyAsync(m => m.MovieName == movieName);
        }
    }
}