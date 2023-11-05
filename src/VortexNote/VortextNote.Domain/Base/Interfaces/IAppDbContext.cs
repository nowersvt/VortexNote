using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VortextNote.Domain.Entities;

namespace VortextNote.Domain.Base.Interfaces
{
    public interface IAppDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chunk> Chunks { get; set; }
        public Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken = default);
    }
}
