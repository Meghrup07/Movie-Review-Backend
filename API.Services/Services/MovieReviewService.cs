using API.Contracts.MovieReview;
using API.Domain.Models;
using API.Infrastructure.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class MovieReviewService(IRepositoryManager repositoryManager, IMapper mapper) : IMovieReviewService
    {
        public async Task<MovieReview> CreateAsync(AddMovieReviewDTO addMovieReviewDTO)
        {
            var user = await repositoryManager.UserRepository.GetByIdAsync(addMovieReviewDTO.UserId);

            if (user == null) throw new Exception("User not found");

            var movie = await repositoryManager.MovieRepository.GetByIdAsync(addMovieReviewDTO.MovieId);

            if (movie == null) throw new Exception("Movie not found");

            var movieReview = mapper.Map<MovieReview>(addMovieReviewDTO);

            await repositoryManager.MovieReviewRepository.CreateAsync(movieReview);

            await repositoryManager.MovieReviewRepository.SaveChangesAsync();

            return movieReview;
        }

        public async Task<MovieReview> DeleteAsync(int id)
        {
            var movieReview = await repositoryManager.MovieReviewRepository.GetByIdAsync(id);
            if (movieReview == null) throw new Exception("MovieReview not found");

            await repositoryManager.MovieReviewRepository.DeleteAsync(movieReview);

            await repositoryManager.MovieReviewRepository.SaveChangesAsync();

            return movieReview;
        }

        public async Task<IEnumerable<MovieReview>> GetAsync()
        {
            var movieReviews = await repositoryManager.MovieReviewRepository.GetAsync();
            return movieReviews;
        }

        public async Task<MovieReview> GetByIdAsync(int id)
        {
            var movieReview = await repositoryManager.MovieReviewRepository.GetByIdAsync(id);
            if (movieReview == null) throw new Exception("MovieReview not found");

            return movieReview;
        }

        public async Task<MovieReview> UpdateAsync(int id, UpdateMovieReviewDTO updateMovieReviewDTO)
        {
            var movieReview = await repositoryManager.MovieReviewRepository.GetByIdAsync(id);
            if (movieReview == null) throw new Exception("MovieReview not found");

            mapper.Map(updateMovieReviewDTO, movieReview);

            await repositoryManager.MovieReviewRepository.UpdateAsync(movieReview);

            await repositoryManager.MovieReviewRepository.SaveChangesAsync();

            return movieReview;
        }
   
    }
}
