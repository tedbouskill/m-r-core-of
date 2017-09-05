using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DomainCore;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryRepository
    {
		Task<IEnumerable<InventoryItem>> InventoryAsync();

        Task<InventoryItem> ItemAsync(Guid id);

        Task AddAsync(InventoryItem inventoryItem);

        Task UpdateAsync(InventoryItem inventoryItem);

		Task DeleteAsync(Guid Id);
    }
}
