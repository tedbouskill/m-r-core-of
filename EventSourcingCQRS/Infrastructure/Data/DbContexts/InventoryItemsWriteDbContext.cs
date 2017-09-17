
using Microsoft.EntityFrameworkCore;

public class InventoryItemsWriteDbContext : DbContext
{
    public InventoryItemsWriteDbContext(DbContextOptions<InventoryItemsWriteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainCore.InventoryItemDto>()
                    .ToTable("InventoryItems")
                    .HasKey(o => new { o.Id });
    }

    public DbSet<DomainCore.InventoryItemDto> InventoryItems { get; set; }
}
