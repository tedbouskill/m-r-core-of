
using Microsoft.EntityFrameworkCore;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainCore.InventoryItemDto>()
                    .ToTable("InventoryItems")
                    .HasKey(o => new { o.Id });
        modelBuilder.Entity<DomainCore.InventoryItemEventDto>()
                    .ToTable("InventoryItemEvents")
                    .HasKey(o => new { o.AggregateId, o.Timestamp });
    }

    public DbSet<DomainCore.InventoryItemDto> InventoryItems { get; set; }
    public DbSet<DomainCore.InventoryItemEventDto> InventoryEventItems { get; set; }
}
