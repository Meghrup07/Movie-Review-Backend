using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.User;
using API.Comman;
using API.Domain.Models;

namespace API.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<UserDetailsDTO>> GetAsync(UserParams userParams);
        Task<AppUser> GetByIdAsync(int id);
        Task DeleteAsync(AppUser user);
        Task<bool> SaveChangesAsync();
    }
}