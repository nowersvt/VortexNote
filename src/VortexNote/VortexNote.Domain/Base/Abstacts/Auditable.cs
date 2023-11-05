using VortextNote.Domain.Base.Interfaces;

namespace VortextNote.Domain.Base.Abstacts
{
    public class Auditable : Identity, IAuditable
    {
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// In future is make if we have global server
        /// </summary>
        //public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        //public string? UpdatedBy { get; set; }
    }
}