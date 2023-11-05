using Microsoft.EntityFrameworkCore;

namespace VortextNote.Domain.Base.Abstacts
{
    public class DbContextBase : Microsoft.EntityFrameworkCore.DbContext 
    {
        public DbContextBase(DbContextOptions options) : base(options)
        { }

        private void DbSaveChanges()
        {

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {   
            DbSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            DbSaveChanges();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            DbSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            DbSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}