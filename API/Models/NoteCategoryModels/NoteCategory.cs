using API.Models.NoteModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.NoteCategoryModels
{
    public class NoteCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [Required]
        [MaxLength(16)]
        public string Name { get; set; }
        public int NotesQuantity { get; set; } = 0;
        public List<Note>? Notes { get; set; } = new List<Note>();
    }
}
