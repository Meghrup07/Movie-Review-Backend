using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MovieReview
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string ReviewerName { get; set; }
        [Required]
        [MaxLength(500)]
        public required string ReviewerComments { get; set; }
        [Required]
        public required int ReviewerRating { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}