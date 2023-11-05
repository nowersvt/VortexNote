using System.ComponentModel.DataAnnotations.Schema;
using VortextNote.Domain.Base.Abstacts;

namespace VortextNote.Domain.Entities
{
    public class Note : Auditable
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public Chunk Chunk { get; set; }
        [ForeignKey(nameof(Chunk))]
        public Guid ChunkId { get; set; }
    }
}
