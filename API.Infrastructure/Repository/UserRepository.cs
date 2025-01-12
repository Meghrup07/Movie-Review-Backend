using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Contracts.User;
using API.Comman;
using API.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using API.Infrastructure.Interfaces;

namespace API.Repository
{
    public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
    {
        public async Task DeleteAsync(AppUser user)
        {
            context.Users.Remove(user);
        }

        public async Task<PagedList<UserDetailsDTO>> GetAsync(UserParams userParams)
        {
            var query = context.Users.AsQueryable();

            if (userParams.Search != null)
            {
                query = query.Where(s => s.UserName.ToLower().Contains(userParams.Search.ToLower())
                || s.Name.ToLower().Contains(userParams.Search.ToLower())
                || s.Email.ToLower().Contains(userParams.Search.ToLower()));
            }

            return await PagedList<UserDetailsDTO>.CreateAsync(query.ProjectTo<UserDetailsDTO>(mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);

        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await context.Users.Include(r => r.Reviews).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}