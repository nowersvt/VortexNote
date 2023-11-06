using System.ComponentModel.DataAnnotations.Schema;
using VortexNote.Domain.Base.Abstacts;

namespace VortexNote.Domain.Entities
{
    public class Note : Auditable
    {
        public Note(string title, string description)
        {
            Title = title;
            Description = description;
        }
        #region props
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public Chunk? Chunk { get; set; }
        [ForeignKey(nameof(Chunk))]
        public Guid? ChunkId { get; set; }
        #endregion
        #region .ctor EF Core
        /// <summary>
        /// .ctor for EF Core
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="userId"></param>
        /// <param name="chunkId"></param>
        public Note(string title, string description, Guid userId, Guid chunkId)
        {
            Title = title;
            Description = description;
            UserId = userId;
            ChunkId = chunkId;
        }
        #endregion
    }
}
