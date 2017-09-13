using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DomainCore;

using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data
{
    /// <summary>
    /// Bridge class for Read/Write Repositories
    /// </summary>
    public class InventoryRepository : IInventoryRepository
    {
        private IInventoryReadRepository _readRepository;
        private IInventoryWriteRepository _writeRepository;

        // ToDo: Add distributed application cache

        public InventoryRepository(IInventoryReadRepository readRepository, IInventoryWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<IEnumerable<InventoryItemDto>> InventoryAsync()
        {
            return await _readRepository.AllAsync();
        }

        public async Task<InventoryItemDto> ItemAsync(Guid id)
        {
            return await _readRepository.ModelAsync(id);
        }

        public async Task AddAsync(InventoryItemDto inventoryItem)
        {
            await _writeRepository.AppendAsync(inventoryItem.Id, inventoryItem);
        }
		
        public async Task UpdateAsync(InventoryItemDto inventoryItem)
		{
			await _writeRepository.UpdateAsync(inventoryItem.Id, inventoryItem);
		}

		public async Task DeleteAsync(Guid id)
		{
            await _writeRepository.DeleteAsync(id);
		}
	}
}
