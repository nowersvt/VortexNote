using Microsoft.EntityFrameworkCore;
using VortexNote.Domain.Base.Interfaces;

namespace VortexNote.Domain.Base.Abstacts
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options) : base(options)
        { }

        private void DbSaveChanges()
        {
            var defaultDate = DateTime.UtcNow;
            var defaultUser = Environment.UserName;
            defaultUser ??= "user";
            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entry in addedEntities)
            {
                if (entry.Entity is not IAuditable)
                {
                    return;
                }

                var createdAt = entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue;
                var createdBy = entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue;

                entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue = defaultUser;

                if (DateTime.Parse(createdAt?.ToString()!).Year < 1970)
                {
                    entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = defaultDate;
                }
            }

            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var updateEntry in modifiedEntities)
            {
                if (updateEntry.Entity is IAuditable)
                {
                    updateEntry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = DateTime.UtcNow;
                }
            }
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            DbSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
        {
            DbSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}