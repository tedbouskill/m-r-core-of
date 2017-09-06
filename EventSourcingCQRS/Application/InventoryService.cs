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
        private IInventoryEventRepository _inventoryEventRepository;

        public InventoryService(IInventoryRepository inventoryRepository, IInventoryEventRepository inventoryEventRepository)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryEventRepository = inventoryEventRepository;
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
            await Task.WhenAll(
                _inventoryEventRepository.AppendEvent(
                        item.Id,
                        new InventoryItemEvent()
                        {
                            AggregateKey = item.Id,
                            TimeStamp = DateTime.UtcNow,
                            Event = "CreateInventoryItem",
                            EventObjTypeName = "InventoryItem",
                            EventObjJson = Newtonsoft.Json.JsonConvert.SerializeObject(item)
                        }
                    ),
                _inventoryRepository.AddAsync(item)
            );
		}

        public async Task PutItemAsync(InventoryItem item)
        {
            await Task.WhenAll(
                _inventoryEventRepository.AppendEvent(
                        item.Id,
                        new InventoryItemEvent()
                        {
                            AggregateKey = item.Id,
                            TimeStamp = DateTime.UtcNow,
                            Event = "UpdateFullInventoryItem",
                            EventObjTypeName = "InventoryItem",
                            EventObjJson = Newtonsoft.Json.JsonConvert.SerializeObject(item)
                        }
                    ),
                _inventoryRepository.UpdateAsync(item)
            );
        }

		public async Task DeleteItemAsync(Guid id)
		{
            await Task.WhenAll(
                _inventoryEventRepository.AppendEvent(
                        id,
                        new InventoryItemEvent()
                        {
                            AggregateKey = id,
                            TimeStamp = DateTime.UtcNow,
                            Event = "DeleteInventoryItem",
                            EventObjTypeName = "Guid",
                            EventObjJson = Newtonsoft.Json.JsonConvert.SerializeObject(id)
                        }
                    ),
                _inventoryRepository.DeleteAsync(id)
            );
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
