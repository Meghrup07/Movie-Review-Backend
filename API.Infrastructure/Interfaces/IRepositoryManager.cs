using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Interfaces
{
    public interface IRepositoryManager
    {
        IMovieRepository MovieRepository { get; }
        IMovieReviewRepository MovieReviewRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
