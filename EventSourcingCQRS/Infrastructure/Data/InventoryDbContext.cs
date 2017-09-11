using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
	{
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.Entity<DomainCore.InventoryItemEventDto>()
                    .ToTable("InventoryItemEvents")
                    .HasKey(o => new { o.AggregateKey, o.Timestamp });
	}

	public DbSet<DomainCore.InventoryItem> InventoryItems { get; set; }

    public DbSet<DomainCore.InventoryItemEventDto> InventoryEventItems { get; set; }
}
