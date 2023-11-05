namespace VortextNote.Domain.ViewModels
{
    public class NoteViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserViewModel User { get; set; }
        public ChunkViewModel Chunk { get; set; }
    }
}