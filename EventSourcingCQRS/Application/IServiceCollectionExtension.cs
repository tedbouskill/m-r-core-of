using Application.Interfaces;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class IServiceCollectionExtension
	{
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
		{
            services.AddInfrastructureServices(configuration);

            // Services
            services.AddScoped<IInventoryCommandHandler, InventoryCommandHandler>();

            services.AddScoped<IInventoryService, InventoryService>();

			return services;
		}
	}
}