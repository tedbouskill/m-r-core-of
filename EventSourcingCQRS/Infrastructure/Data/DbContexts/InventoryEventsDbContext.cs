
using Microsoft.EntityFrameworkCore;

public class InventoryEventsDbContext : DbContext
{
    public InventoryEventsDbContext(DbContextOptions<InventoryEventsDbContext> options)
        : base(options)
	{
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<DomainCore.InventoryItemEventDto>()
                    .ToTable("InventoryItemEvents")
                    .HasKey(o => new { o.AggregateId, o.Timestamp });
	}

    public DbSet<DomainCore.InventoryItemEventDto> InventoryEventItems { get; set; }
}
