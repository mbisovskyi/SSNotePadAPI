using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.Models
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [ForeignKey(nameof(NoteId))] public int NoteId { get; set; }
        [MaxLength(24)] public string Title { get; set; } = "";

    }
}
