using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	public static class IServiceCollectionExtension
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
            // Data Services
			services.AddDbContext<InventoryDbContext>(options =>
					options.UseSqlite("Data Source=./inventory.sqlite"));

            return services;
		}
	}
}