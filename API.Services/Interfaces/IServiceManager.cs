using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IServiceManager
    {
        IMovieService MovieService { get; }
        IMovieReviewService MovieReviewService { get; }
        IUserService UserService { get; }
    }
}
