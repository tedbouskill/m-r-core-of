using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class IServiceCollectionExtension
	{
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
		{

			var itemsOptions = new DbContextOptionsBuilder<InventoryItemsDbContext>()
				.UseSqlite(new SqliteConnection(configuration.GetConnectionString("InventoryDbContext")))
				.Options;

			// Create the schema in the database
			using (var dbContext = new InventoryItemsDbContext(itemsOptions))
			{
				dbContext.Database.EnsureCreated();
			}

            var eventsOptions = new DbContextOptionsBuilder<InventoryEventsDbContext>()
            	.UseSqlite(new SqliteConnection(configuration.GetConnectionString("InventoryDbContext")))
            	.Options;

			// Create the schema in the database
            using(var dbContext = new InventoryEventsDbContext(eventsOptions))
            {
				dbContext.Database.EnsureCreated();
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

            // Services
            services.AddScoped<IInventoryCommandHandler, InventoryCommandHandler>();

            services.AddScoped<IInventoryService, InventoryService>();

			return services;
		}
	}
}