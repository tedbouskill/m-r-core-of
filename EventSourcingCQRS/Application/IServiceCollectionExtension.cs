using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
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

			// Data Services
			services.AddDbContext<InventoryDbContext>(options =>
					options.UseSqlite(configuration.GetConnectionString("InventoryDbContext")));

            services.AddTransient<IInventoryEventRepository, InventoryEventRepository>();

			services.AddTransient<IInventoryReadRepository, InventoryReadRepository>();
			services.AddTransient<IInventoryWriteRepository, InventoryWriteRepository>();

			services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddTransient<IInventoryCommandHandler, InventoryCommandHandler>();

            services.AddTransient<IInventoryService, InventoryService>();

			return services;
		}
	}
}