using System.ComponentModel.DataAnnotations.Schema;
using VortextNote.Domain.Base.Abstacts;

namespace VortextNote.Domain.Entities
{
    public class Chunk : Auditable
    {
        public ICollection<Note> Notes { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
    }
}
