using System.ComponentModel.DataAnnotations.Schema;
using VortextNote.Domain.Base.Interfaces;

namespace VortextNote.Domain.Entities
{
    public class User : IHaveId
    {
        public Guid Id { get; set; }
        [InverseProperty(nameof(Note.User))]
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        [InverseProperty(nameof(Chunk.User))]
        public ICollection<Chunk> Chunks { get; set; } = new List<Chunk>();
    }
}
