using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DomainCore;

namespace Infrastructure.Data
{
    public class InventoryReadRepository : Interfaces.IInventoryReadRepository
    {
		private readonly InventoryDbContext _dbContext;

		public InventoryReadRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InventoryItem>> AllAsync()
        {
            return await _dbContext.InventoryItems.ToListAsync();
        }

        public async Task<InventoryItem> ModelAsync(Guid id)
        {
			return await _dbContext.InventoryItems.SingleOrDefaultAsync(m => m.Id == id);
        }
	}
}
