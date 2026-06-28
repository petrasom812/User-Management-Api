using System.ComponentModel.DataAnnotations;

namespace UserSystem.Api.Dtos
{
    public class GetAllUsersDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}