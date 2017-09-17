
using Microsoft.EntityFrameworkCore;

public class InventoryItemsReadDbContext : DbContext
{
    public InventoryItemsReadDbContext(DbContextOptions<InventoryItemsReadDbContext> options)
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
