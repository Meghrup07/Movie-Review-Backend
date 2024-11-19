using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Movie
{
    public class AddMovieDTO
    {
        [Required]
        [MaxLength(255)]
        public required string MovieName { get; set; }
        [Required]
        [MaxLength(255)]
        public required string DirectorName { get; set; }
        [Required]
        [MaxLength(10)]
        public required string ReleaseYear { get; set; }
        [Required]
        public required IFormFile MoviePic { get; set; }
    }
}