using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> users { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<MovieReview> movieReviews { get; set; }
    }
}