﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VortexNote.Domain.Base.Abstacts;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Domain.Entities;

namespace VortexNote.Infrastructure.Persistence
{
    public class AppDbContext : DbContextBase, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chunk> Chunks { get; set; }

        public async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }
    }
}