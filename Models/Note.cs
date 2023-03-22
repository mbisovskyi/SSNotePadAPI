using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.Models
{
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [ForeignKey(nameof(UserId))] public int UserId { get; set; }
        [ForeignKey(nameof(CategoryId))] public int CategoryId { get; set; }
        [MaxLength(100)] public string Title { get; set; } = "";
        [MaxLength(2000)] public string Description { get; set; } = "";
        [AllowNull, NotNull] public DateTime? DateCreated { get; set; } = null;
        public List<Image> Images { get; set; } = new List<Image>();


    }
}
