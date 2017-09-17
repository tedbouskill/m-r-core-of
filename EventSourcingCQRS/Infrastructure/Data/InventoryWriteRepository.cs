using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DomainCore;

namespace Infrastructure.Data
{
    public class InventoryWriteRepository : Interfaces.IInventoryWriteRepository
    {
		private readonly InventoryItemsWriteDbContext _dbContext;

		public InventoryWriteRepository(InventoryItemsWriteDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task AppendAsync(Guid id, InventoryItemDto model)
        {
            _dbContext.InventoryItems.Add(model);

			await _dbContext.SaveChangesAsync();
		}

        public async Task DeleteAsync(Guid id)
        {
            var inventoryItem = await _dbContext.InventoryItems.SingleOrDefaultAsync(m => m.Id == id);

            _dbContext.InventoryItems.Remove(inventoryItem);
			
            await _dbContext.SaveChangesAsync();
		}

        public async Task UpdateAsync(Guid id, InventoryItemDto model)
        {
			try
			{
                _dbContext.InventoryItems.Update(model);

				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
                if (_dbContext.InventoryItems.Any(e => e.Id == id))
				{
					throw;
				}
			}
		}
    }
}
