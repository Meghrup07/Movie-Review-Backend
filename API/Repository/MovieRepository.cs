using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.DTOs.Movie;
using API.Helpers;
using API.Interface;
using API.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class MovieRepository(DataContext context, IMapper mapper) : IMovieRepository
    {
        public async Task CreateAsync(Movie movie)
        {
            await context.movies.AddAsync(movie);
        }

        public async Task DeleteAsync(Movie movie)
        {
            context.movies.Remove(movie);
        }

        public async Task<PagedList<MovieDTO>> GetAsync(UserParams userParams)
        {
            var query = context.movies.AsQueryable();

            if (userParams.Search != null)
            {
                query = query.Where(s => s.MovieName.ToLower().Contains(userParams.Search.ToLower()));
            }

            return await PagedList<MovieDTO>.CreateAsync(query.ProjectTo<MovieDTO>(mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);

        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await context.movies.Include(m => m.Reviews).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;
        }
    }
}