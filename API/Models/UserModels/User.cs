using API.Models.NoteCategoryModels;
using API.Models.NoteModels;
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
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(16)]
        [NotNull]
        public string Password { get; set; } = string.Empty;
        [NotNull]
        public string? Email { get; set; } = string.Empty;
        public List<Note>? Notes { get; set; }
        public List<NoteCategory>? NoteCategories { get; set; } = new() { 
            new NoteCategory() 
            { 
                Id = 0, Name = "General", 
                Notes = new List<Note>(), 
                NotesQuantity = 0
            } 
        };
    }
}
