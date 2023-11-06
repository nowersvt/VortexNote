using VortexNote.Domain.Base.Interfaces;

namespace VortexNote.Domain.Base.Abstacts
{
    public class Identity : IHaveId
    {
        public Guid Id { get; set; }
    }
}