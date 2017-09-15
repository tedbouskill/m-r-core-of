using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DomainCore;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryRepository
    {
		Task<IEnumerable<InventoryItemDto>> InventoryAsync();

        Task<InventoryItemDto> ItemAsync(Guid id);

        Task AddAsync(InventoryItemDto inventoryItem);

        Task UpdateAsync(InventoryItemDto inventoryItem);

		Task DeleteAsync(Guid Id);
    }
}
