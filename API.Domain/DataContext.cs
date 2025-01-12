using API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }
    }
}
