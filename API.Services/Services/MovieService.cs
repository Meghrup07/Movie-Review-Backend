using API.Comman;
using API.Contracts.Movie;
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
    public class MovieService(IRepositoryManager repositoryManager, IMapper mapper) : IMovieService
    {
        public async Task<Movie> CreateAsync(AddMovieDTO addMovieDTO)
        {
            var movie = mapper.Map<Movie>(addMovieDTO);
            
            await repositoryManager.MovieRepository.CreateAsync(movie);

            await repositoryManager.MovieRepository.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie> DeleteAsync(int id)
        {
            var movie = await repositoryManager.MovieRepository.GetByIdAsync(id);

            if (movie == null) throw new Exception("Movie not found");

            await repositoryManager.MovieRepository.DeleteAsync(movie);

            await repositoryManager.MovieRepository.SaveChangesAsync();

            return movie;
        }

        public async Task<PaginationResponse<MovieDTO>> GetAsync(UserParams userParams)
        {
            var movies = await repositoryManager.MovieRepository.GetAsync(userParams);

            var response = new PaginationResponse<MovieDTO>
            {
                Items = movies,
                TotalCount = movies.Count
            };

            return response;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await repositoryManager.MovieRepository.GetByIdAsync(id);

            if (movie == null) throw new Exception("Movie not found");

            return movie;
        }

        public async Task<Movie> UpdateAsync(int id, UpdateMovieDTO updateMovieDTO)
        {
            var movie = await repositoryManager.MovieRepository.GetByIdAsync(id);

            if (movie == null) throw new Exception("Movie not found");

            mapper.Map<Movie>(updateMovieDTO);

            await repositoryManager.MovieRepository.UpdateAsync(movie);

            await repositoryManager.MovieRepository.SaveChangesAsync();

            return movie;
        }
    }
}
