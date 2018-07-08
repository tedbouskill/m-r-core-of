using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class IServiceCollectionExtension
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
            {
                var options = new DbContextOptionsBuilder<InventoryDbContext>()
                    .UseSqlite(new SqliteConnection(configuration.GetConnectionString("InventoryDbContext")))
                    .Options;

                // Create the schema in the database
                using (var dbContext = new InventoryDbContext(options))
                {
                    dbContext.Database.EnsureCreated();
                }
            }

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