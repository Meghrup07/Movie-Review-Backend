using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Movie;
using API.Helpers;
using API.Models;

namespace API.Interface
{
    public interface IMovieRepository
    {
        Task<PagedList<MovieDTO>> GetAsync(UserParams userParams);
        Task<Movie> GetByIdAsync(int id);
        Task CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);
        Task<bool> SaveChangesAsync();
    }
}