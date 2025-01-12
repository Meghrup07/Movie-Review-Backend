using API.Comman;
using API.Contracts.Movie;
using API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IMovieService
    {
        Task<PaginationResponse<MovieDTO>> GetAsync(UserParams userParams);
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> CreateAsync(AddMovieDTO addMovieDTO);
        Task<Movie> UpdateAsync(int id, UpdateMovieDTO updateMovieDTO);
        Task<Movie> DeleteAsync(int id);
    }
}
