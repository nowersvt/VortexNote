namespace VortextNote.Domain.ViewModels
{
    public class ChunkViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<NoteViewModel> Notes { get; set; }
        public UserViewModel User { get; set; }
    }
}