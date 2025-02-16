using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uniPoint_backend.Models
{
    public enum UserRole
    {
        USER,
        ADMIN,
        PROVIDER
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(20)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(255)]
        public string ProfilePictureUrl { get; set; } = "default.png";

        [Required]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; } = UserRole.USER;

        [Required]
        [Timestamp]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
