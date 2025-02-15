using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter Valid Email!")]
        public required string Email { get; set; }
        [Required]
        public required string Role { get; set; }
        [Required]
        public required byte[] PasswordHash { get; set; }
        [Required]
        public required byte[] PasswordSalt { get; set; }
        public List<MovieReview> Reviews { get; set; } = [];
    }
}