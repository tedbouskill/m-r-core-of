using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Interfaces;
using DomainCore;
using DomainCore.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Application
{
    public class InventoryService : IInventoryService
    {
        private IInventoryReadRepository _inventoryReadRepository;
        private IInventoryEventRepository _inventoryEventRepository;
        private IInventoryCommandHandler _inventoryCommandHandler;

        public InventoryService(
            IInventoryReadRepository inventoryReadRepository,
            IInventoryEventRepository inventoryEventRepository,
            IInventoryCommandHandler inventoryCommandHandler
        )
        {
            _inventoryReadRepository = inventoryReadRepository;
            _inventoryEventRepository = inventoryEventRepository;
            _inventoryCommandHandler = inventoryCommandHandler;
        }

		public async Task<IEnumerable<InventoryItemDto>> InventoryAsync()
		{
			return await _inventoryReadRepository.AllAsync();
		}

		public async Task<InventoryItemDto> GetItemAsync(Guid id)
		{
            return await _inventoryReadRepository.ModelAsync(id);
		}

        public async Task<IEnumerable<AInventoryItemEvent>> InventoryEventsAsync(Guid id)
        {
			InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, id);

            var result = iie.EventsAsync().Result;

            return await Task.FromResult(result.Cast<AInventoryItemEvent>());
		}

		public async Task PostItemAsync(InventoryItemDto item)
		{
            await _inventoryCommandHandler.Handle(new CreateInventoryItem(item));
		}

        public async Task PutItemAsync(InventoryItemDto item)
        {
            await _inventoryCommandHandler.Handle(new UpdateInventoryItem(item));
		}

		public async Task DeleteItemAsync(Guid id)
		{
            await _inventoryCommandHandler.Handle(new DeleteInventoryItem(id));
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
