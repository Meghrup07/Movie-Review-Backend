using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using API.Infrastructure.Interfaces;

namespace API.Repository
{
    public class MovieReviewRepository(DataContext context) : IMovieReviewRepository
    {
        public async Task CreateAsync(MovieReview movieReview)
        {
            await context.MovieReviews.AddAsync(movieReview);
        }

        public async Task DeleteAsync(MovieReview movieReview)
        {
            context.MovieReviews.Remove(movieReview);
        }

        public async Task<IEnumerable<MovieReview>> GetAsync()
        {
            return await context.MovieReviews.ToListAsync();
        }

        public async Task<MovieReview> GetByIdAsync(int id)
        {
            return await context.MovieReviews.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(MovieReview movieReview)
        {
            context.Entry(movieReview).State = EntityState.Modified;
        }
    }
}