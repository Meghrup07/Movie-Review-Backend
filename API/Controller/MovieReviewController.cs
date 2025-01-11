using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.DTOs.MovieReview;
using API.Interface;
using API.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieReviewController(DataContext context, IMovieReviewRepository movieReviewRepository, IMovieRepository movieRepository, IMapper mapper, IUserRepository userRepository) : ControllerBase
    {
        [HttpGet("get/movie/reviews")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieReview>> GetMovieReviews()
        {
            var movies = await movieReviewRepository.GetAsync();

            return Ok(movies);
        }


        [HttpGet("get/movie/review/{id:int}")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReview>> GetMovieReview([FromRoute] int id)
        {
            var movieReview = await movieReviewRepository.GetByIdAsync(id);

            if (movieReview == null) return NotFound("Movie Review Not Found!");

            return Ok(movieReview);
        }


        [HttpPost("add/movie/review")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieReview>> AddMovieReview(AddMovieReviewDTO addMovieReviewDTO)
        {

            var movie = await movieRepository.GetByIdAsync(addMovieReviewDTO.MovieId);

            if (movie == null) return NotFound("Movie Not Found!");

            var user = await userRepository.GetByIdAsync(addMovieReviewDTO.UserId);

            if (user == null) return NotFound("User Not Found!");

            var movieReview = mapper.Map<MovieReview>(addMovieReviewDTO);
            addMovieReviewDTO.UserId = user.Id;
            addMovieReviewDTO.MovieId = movie.Id;


            await movieReviewRepository.CreateAsync(movieReview);

            await movieReviewRepository.SaveChangesAsync();

            return Ok(movieReview);
        }


        [HttpPut("edit/movie/review/{id:int}")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReview>> UpdateMovie([FromRoute] int id, UpdateMovieReviewDTO updateMovieReviewDTO)
        {
            var movieReview = await movieReviewRepository.GetByIdAsync(id);

            if (movieReview == null) return NotFound("Movie Review Not Found!");

            mapper.Map(updateMovieReviewDTO, movieReview);

            await movieReviewRepository.UpdateAsync(movieReview);

            await movieReviewRepository.SaveChangesAsync();

            return Ok(movieReview);
        }

        [HttpDelete("delete/movie/review/{id:int}")]
        [ProducesResponseType(typeof(MovieReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReview>> DeleteMovieReview([FromRoute] int id)
        {
            var movieReview = await movieReviewRepository.GetByIdAsync(id);

            if (movieReview == null) return NotFound("Movie Review Not Found!");

            await movieReviewRepository.DeleteAsync(movieReview);

            await movieReviewRepository.SaveChangesAsync();

            return Ok(movieReview);
        }

    }
}