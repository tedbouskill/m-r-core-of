using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Application.Interfaces;

using Infrastructure.Data;
using Infrastructure.Data.Interfaces;

namespace Application
{
	public static class IServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			// Data Services
			services.AddDbContext<InventoryDbContext>(options =>
					options.UseSqlite("Data Source=./inventory.sqlite"));

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