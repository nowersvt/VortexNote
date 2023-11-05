using System.ComponentModel.DataAnnotations;

namespace VortextNote.Domain.Base.Interfaces
{
    public interface IHaveId
    {
        [Key]
        public Guid Id { get; set; }
    }
}
