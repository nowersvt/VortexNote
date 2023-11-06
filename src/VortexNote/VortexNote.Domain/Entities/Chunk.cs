using System.ComponentModel.DataAnnotations.Schema;
using VortexNote.Domain.Base.Abstacts;

namespace VortexNote.Domain.Entities
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
