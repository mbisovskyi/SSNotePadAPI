using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.NoteImageModels
{
    public class NoteImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(NoteId))]
        public int NoteId { get; set; }
        [Required]
        [MaxLength(16)]
        public string Name { get; set; }
    }
}
