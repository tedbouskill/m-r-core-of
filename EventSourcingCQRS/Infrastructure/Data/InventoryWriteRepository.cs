using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DomainCore;

namespace Infrastructure.Data
{
    public class InventoryWriteRepository : Interfaces.IInventoryWriteRepository
    {
		private readonly InventoryDbContext _dbContext;

		public InventoryWriteRepository(InventoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task Append(Guid key, InventoryItem model)
        {
            _dbContext.InventoryItems.Add(model);

			await _dbContext.SaveChangesAsync();
		}

        public async Task Delete(Guid key)
        {
            var inventoryItem = await _dbContext.InventoryItems.SingleOrDefaultAsync(m => m.Id == key);

            _dbContext.InventoryItems.Remove(inventoryItem);
			
            await _dbContext.SaveChangesAsync();
		}

        public async Task Update(Guid key, InventoryItem model)
        {
			try
			{
                _dbContext.InventoryItems.Update(model);

				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
                if (_dbContext.InventoryItems.Any(e => e.Id == key))
				{
					throw;
				}
			}
		}
    }
}
