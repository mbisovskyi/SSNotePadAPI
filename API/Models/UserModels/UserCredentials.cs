using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.Models.UserModels
{
    public class UserCredentials
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [Required]
        [MaxLength(24)]
        [NotNull]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(16)]
        [NotNull]
        public string Password { get; set; } = string.Empty;
        [NotNull]
        public string? Email { get; set; } = string.Empty;
    }
}
