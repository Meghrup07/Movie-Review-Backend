using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Infrastructure.Interfaces
{
    public interface IMovieReviewRepository
    {
        Task<IEnumerable<MovieReview>> GetAsync();
        Task<MovieReview> GetByIdAsync(int id);
        Task CreateAsync(MovieReview movieReview);
        Task UpdateAsync(MovieReview movieReview);
        Task DeleteAsync(MovieReview movieReview);
        Task<bool> SaveChangesAsync();
    }
}