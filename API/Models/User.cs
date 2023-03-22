using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [MaxLength(24)] public string? Username { get; set; } = string.Empty;
        [MaxLength(16)] public string? Password { get; set; } = string.Empty;
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
