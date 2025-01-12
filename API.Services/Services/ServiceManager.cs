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
    public class ServiceManager : IServiceManager
    {
        public readonly Lazy<IMovieService> _movieService;
        public readonly Lazy<IMovieReviewService> _movieReviewService;
        public readonly Lazy<IUserService> _userService;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _movieService = new Lazy<IMovieService>(() => new MovieService(repositoryManager, mapper));
            _movieReviewService = new Lazy<IMovieReviewService>(() => new MovieReviewService(repositoryManager, mapper));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        }

        public IMovieService MovieService => _movieService.Value;

        public IMovieReviewService MovieReviewService => _movieReviewService.Value;

        public IUserService UserService => _userService.Value;
    }
}
