using API.Domain;
using API.Infrastructure.Interfaces;
using API.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        public readonly Lazy<IMovieRepository>  _movieRepository;
        public readonly Lazy<IMovieReviewRepository> _movieReviewRepository;
        public readonly Lazy<IUserRepository> _userRepository;

        public RepositoryManager(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _movieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(_context, _mapper));
            _movieReviewRepository = new Lazy<IMovieReviewRepository>(() => new MovieReviewRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context, _mapper));

        }
        public IMovieRepository MovieRepository => _movieRepository.Value;

        public IMovieReviewRepository MovieReviewRepository => _movieReviewRepository.Value;

        public IUserRepository UserRepository => _userRepository.Value;
    }
}
