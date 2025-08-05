using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Persistence.Context;
using SampleWithCustomStore.Entities;

namespace SampleWithCustomStore.Context;

public class SampleCustomDbContext(DbContextOptions<SampleCustomDbContext> options) : NavigatorStoreDbContext(options)
{
    public required DbSet<MessageCount> MessageCounts { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var messageCountEntity = modelBuilder.Entity<MessageCount>();

        messageCountEntity
            .HasKey(e => e.Id);
        
        messageCountEntity
            .Property(e => e.Count)
            .IsRequired();

        messageCountEntity
            .HasOne(e => e.User);
    }

}