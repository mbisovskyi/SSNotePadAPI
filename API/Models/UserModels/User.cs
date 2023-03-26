using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.Models.UserModels
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(24)]
        public string FirstName { get; set; } = string.Empty;
        [NotNull]
        public bool IsOwnerOperator { get; set; } = false;
        [Required]
        public UserCredentials Credentials { get; set; } = new UserCredentials();
    }
}
