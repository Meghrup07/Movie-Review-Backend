using API.Comman;
using API.Contracts.User;
using API.Domain.Models;
using API.Infrastructure.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
    {
        public async Task<AppUser> DeleteAsync(int id)
        {
            var user = await repositoryManager.UserRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            await repositoryManager.UserRepository.DeleteAsync(user);

            await repositoryManager.UserRepository.SaveChangesAsync();

            return user;
        }

        public async Task<PaginationResponse<UserDetailsDTO>> GetAsync(UserParams userParams)
        {
            var users = await repositoryManager.UserRepository.GetAsync(userParams);

            var response = new PaginationResponse<UserDetailsDTO>
            {
                Items = users,
                TotalCount = users.Count
            };

            return response;
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            var user = await repositoryManager.UserRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            return user;
        }
    }
}
