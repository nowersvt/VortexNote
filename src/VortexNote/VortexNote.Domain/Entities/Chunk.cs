using System.ComponentModel.DataAnnotations.Schema;
using VortextNote.Domain.Base.Abstacts;

namespace VortextNote.Domain.Entities
{
    public class Chunk : Auditable
    {
        public Chunk(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        [InverseProperty(nameof(Note.Chunk))]
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
    }
}
