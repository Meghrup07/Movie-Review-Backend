using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.MovieReview;

namespace API.DTOs.Movie
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string? MovieName { get; set; }
        public string? DirectorName { get; set; }
        public string? ReleaseYear { get; set; }
        public string? MoviePic { get; set; }
        public List<MovieReviewDTO> Reviews { get; set; } = [];
    }
}