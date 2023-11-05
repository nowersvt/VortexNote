using VortextNote.Domain.Base.Interfaces;

namespace VortextNote.Domain.Entities
{
    public class User : IHaveId
    {
        public Guid Id { get; set; }
        //public string? Email { get; set; }
    }
}
