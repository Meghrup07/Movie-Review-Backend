using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MovieReview
{
    public class UpdateMovieReviewDTO
    {
        [Required]
        public required string ReviewerName { get; set; }
        [Required]
        [MaxLength(500)]
        public required string ReviewerComments { get; set; }
        [Required]
        public required int ReviewerRating { get; set; }

    }
}