using System.ComponentModel.DataAnnotations;

namespace VortexNote.Domain.Base.Interfaces
{
    public interface IHaveId
    {
        [Key]
        public Guid Id { get; set; }
    }
}
