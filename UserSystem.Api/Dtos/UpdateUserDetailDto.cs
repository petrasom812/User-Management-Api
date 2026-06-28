using System.ComponentModel.DataAnnotations;

namespace UserSystem.Api.Dtos
{
    public class UpdateUserDetailDto
    {
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string Role { get; set; } = string.Empty;
        public DateTime? EditedAt {get; set;}
    }
}