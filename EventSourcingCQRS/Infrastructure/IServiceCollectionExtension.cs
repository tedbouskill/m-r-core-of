using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class IServiceCollectionExtension
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			// DbContexts
			services.AddDbContext<InventoryItemsDbContext>(options =>
					options.UseSqlite(configuration.GetConnectionString("InventoryDbContext")));

			services.AddDbContext<InventoryItemsReadDbContext>(options =>
					options.UseSqlite(configuration.GetConnectionString("InventoryDbContext")));
			services.AddDbContext<InventoryItemsWriteDbContext>(options =>
					options.UseSqlite(configuration.GetConnectionString("InventoryDbContext")));

			services.AddDbContext<InventoryEventsDbContext>(options =>
					options.UseSqlite(configuration.GetConnectionString("InventoryDbContext")));

            // Repositories
			services.AddScoped<IInventoryEventRepository, InventoryEventRepository>();

			services.AddScoped<IInventoryReadRepository, InventoryReadRepository>();
			services.AddScoped<IInventoryWriteRepository, InventoryWriteRepository>();

			services.AddScoped<IInventoryRepository, InventoryRepository>();

			return services;
		}
	}
}