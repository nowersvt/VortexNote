using VortextNote.Domain.Base.Interfaces;

namespace VortextNote.Domain.Base.Abstacts
{
    public class Identity : IHaveId
    {
        public Guid Id { get; set; }
    }
}