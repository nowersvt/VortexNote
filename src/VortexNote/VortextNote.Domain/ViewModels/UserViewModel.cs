namespace VortextNote.Domain.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public ICollection<NoteViewModel> Notes { get; set; } = new List<NoteViewModel>();
        public ICollection<ChunkViewModel> Chunks { get; set; } = new List<ChunkViewModel>();
    }
}