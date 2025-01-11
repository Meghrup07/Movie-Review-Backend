using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.User;
using API.Helpers;
using API.Domain.Models;

namespace API.Interface
{
    public interface IUserRepository
    {
        Task<PagedList<UserDetailsDTO>> GetAsync(UserParams userParams);
        Task<AppUser> GetByIdAsync(int id);
        Task DeleteAsync(AppUser user);
        Task<bool> SaveChangesAsync();
    }
}