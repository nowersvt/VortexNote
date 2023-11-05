using VortextNote.Domain.Base.Interfaces;

namespace VortextNote.Domain.Base.Abstacts
{
    public class Auditable : Identity, IAuditable<string>
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}