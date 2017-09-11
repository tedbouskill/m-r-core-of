using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Interfaces;

using Common.EventSourcing.Interfaces;

using DomainCore;
using DomainCore.EventData;

using Infrastructure.Data.Interfaces;

namespace Application
{
    public class InventoryService : IInventoryService
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

        public async Task<IEnumerable<InventoryItemEvent>> InventoryEventsAsync(Guid id)
        {
			InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, id);

            var result = iie.EventsAsync().Result.Cast<InventoryItemEvent>();

            return await Task.FromResult(result);
                            //.ContinueWith<IEnumerable<InventoryItemEvent>>(t => (IEnumerable<InventoryItemEvent>)t);
		}

		public async Task PostItemAsync(InventoryItem item)
		{
            InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, item.Id);

            await Task.WhenAll(
                iie.AppendEventAsync(
                        new InventoryItemEvent()
                        {
                            AggregateKey = item.Id,
                            Timestamp = DateTime.UtcNow,
                            EventName = "CreateInventoryItem",
                            EventData = new UpsertInventoryItem(item)
                        }
                    ),
                _inventoryRepository.AddAsync(item)
            );
		}

        public async Task PutItemAsync(InventoryItem item)
        {
			InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, item.Id);

			await Task.WhenAll(
				iie.AppendEventAsync(
						new InventoryItemEvent()
						{
							AggregateKey = item.Id,
							Timestamp = DateTime.UtcNow,
							EventName = "UpdateInventoryItem",
							EventData = new UpsertInventoryItem(item)
						}
					),
				_inventoryRepository.UpdateAsync(item)
            );
        }

		public async Task DeleteItemAsync(Guid id)
		{
			InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, id);

			await Task.WhenAll(
				iie.AppendEventAsync(
						new InventoryItemEvent()
						{
							AggregateKey = id,
							Timestamp = DateTime.UtcNow,
							EventName = "DeleteInventoryItem",
							EventData = null
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
