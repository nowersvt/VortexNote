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

            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entry in addedEntities)
            {
                if (entry is not IAuditable)
                {
                    return;
                }

                var createdAt = entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue;
                var updatedAt = entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue;

                if (DateTime.Parse(createdAt?.ToString()!).Year < 1970)
                {
                    entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = defaultDate;
                }

                if (updatedAt != null && DateTime.Parse(updatedAt.ToString()!).Year < 1970)
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
                }
                else
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
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