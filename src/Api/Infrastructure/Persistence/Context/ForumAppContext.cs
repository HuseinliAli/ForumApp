using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context;

public class ForumAppContext : DbContext
{
    public const string DEFAULT_SHCEMA = "dbo";
    public ForumAppContext(DbContextOptions options) : base(options)
    {

    }
    public ForumAppContext()
    {

    }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Entry> Entries { get; set; }
    public DbSet<EntryVote> EntryVotes { get; set; }
    public DbSet<EntryFavorite> EntryFavorites { get; set; }

    public DbSet<EntryComment> EntryComments { get; set; }
    public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
    public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = @"Server=(localdb)\\MSSQLLocalDB;Database=forum_app;Trusted_Connection=true";
            optionsBuilder.UseSqlServer(connectionString, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries()
                                .Where(x => x.State==EntityState.Added)
                                .Select(x => (BaseEntity)x.Entity);
        PrepareToAdd(addedEntities);
    }
    private void PrepareToAdd(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreatedAt ==DateTime.MinValue)
            {
                entity.CreatedAt = DateTime.Now;
            }
        }
    }
}
