namespace VortexNote.Domain.Base.Interfaces
{
    public interface IAuditable<TName>
    {
        public DateTime CreatedAt { get; set; }
        public TName CreatedBy { get; set; }  
        public DateTime? UpdatedAt { get; set; }
        //public TName? UpdatedBy { get; set; }
    }
    public interface IAuditable : IAuditable<string>
    {

    }
}
