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
		private readonly InventoryItemsReadDbContext _dbContext;

		public InventoryReadRepository(InventoryItemsReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InventoryItemDto>> AllAsync()
        {
            return await _dbContext.InventoryItems.ToListAsync();
        }

        public async Task<InventoryItemDto> ModelAsync(Guid id)
        {
			return await _dbContext.InventoryItems.SingleOrDefaultAsync(m => m.Id == id);
        }
	}
}
