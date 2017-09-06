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
		modelBuilder.Entity<DomainCore.InventoryItemEvent>()
                    .HasKey(o => new { o.AggregateKey, o.TimeStamp });
	}

	public DbSet<DomainCore.InventoryItem> InventoryItems { get; set; }

    public DbSet<DomainCore.InventoryItemEvent> InventoryEventItems { get; set; }
}
