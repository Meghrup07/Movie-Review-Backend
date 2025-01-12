using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.Movie;
using API.Comman;
using API.Domain.Models;

namespace API.Infrastructure.Interfaces
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