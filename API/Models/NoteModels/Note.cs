using API.Models.NoteImageModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.Models.NoteModels
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [ForeignKey(nameof(NoteCategoryId))]
        public int NoteCategoryId { get; set; }
        [Required]
        [MaxLength(64)]
        public string Title { get; set; }
        [AllowNull]
        public string? Description { get; set; }
        [Required]
        public string? DateCreated {get; set;}
        public List<NoteImage>? Images { get; set; }
    }
}
