using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [ForeignKey(nameof(UserId))] public int UserId { get; set; }
        [MaxLength(24)] public string Title { get; set; } = "";
        public int NotesQuantity { get; set; } = 0;
        public List<Note> Notes { get; set; } = new List<Note>();

    }
}
