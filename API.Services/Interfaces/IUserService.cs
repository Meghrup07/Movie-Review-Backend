using API.Comman;
using API.Contracts.User;
using API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResponse<UserDetailsDTO>> GetAsync(UserParams userParams);
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> DeleteAsync(int id);
    }
}
