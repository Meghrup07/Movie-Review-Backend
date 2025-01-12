using API.Contracts.MovieReview;
using API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IMovieReviewService
    {
        Task<IEnumerable<MovieReview>> GetAsync();
        Task<MovieReview> GetByIdAsync(int id);
        Task<MovieReview> CreateAsync(AddMovieReviewDTO addMovieReviewDTO);
        Task<MovieReview> UpdateAsync(int id, UpdateMovieReviewDTO updateMovieReviewDTO);
        Task<MovieReview> DeleteAsync(int id);
    }
}
