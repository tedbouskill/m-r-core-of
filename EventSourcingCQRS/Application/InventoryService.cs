using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DomainCore;
using Infrastructure.Data.Interfaces;

namespace Application
{
    public class InventoryService : Interfaces.IInventoryService
    {
        private IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

		public async Task<IEnumerable<InventoryItem>> InventoryAsync()
		{
			return await _inventoryRepository.InventoryAsync();
		}

		public async Task<InventoryItem> GetItemAsync(Guid id)
		{
            return await _inventoryRepository.ItemAsync(id);
		}

        public async Task PostItemAsync(InventoryItem item)
		{
            await _inventoryRepository.AddAsync(item);
		}

        public async Task PutItemAsync(InventoryItem item)
        {
            await _inventoryRepository.UpdateAsync(item);
        }

		public async Task DeleteItemAsync(Guid id)
		{
            await _inventoryRepository.DeleteAsync(id);
		}

		public Task PatchItemCountAsync(Guid id, int count, string reason)
        {
            throw new NotImplementedException();
        }

        public Task PatchItemNameAsync(Guid id, string name, string reason)
        {
            throw new NotImplementedException();
        }

        public Task PatchItemNoteAsync(Guid id, string note, string reason)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseInventory(Guid id, uint amount, string reason)
        {
            throw new NotImplementedException();
        }

        public Task DecreaseInventory(Guid id, uint amount, string reason)
        {
            throw new NotImplementedException();
        }

        public Task ActivateItem(Guid id, string reason)
        {
            throw new NotImplementedException();
        }

        public Task DisableItem(Guid id, string reason)
        {
            throw new NotImplementedException();
        }
    }
}
