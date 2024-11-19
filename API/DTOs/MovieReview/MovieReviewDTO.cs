using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MovieReview
{
    public class MovieReviewDTO
    {

        public int Id { get; set; }
        public string? ReviewerName { get; set; }
        public string? ReviewerComments { get; set; }
        public int? ReviewerRating { get; set; }
    }
}