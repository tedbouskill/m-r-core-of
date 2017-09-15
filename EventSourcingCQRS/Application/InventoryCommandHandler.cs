using System;
using System.Threading.Tasks;
using Application.Commands;
using Application.EventData;
using Application.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;
using Infrastructure.Data.Interfaces;

namespace Application
{
    public class InventoryCommandHandler : IInventoryCommandHandler
	{
		private IInventoryEventRepository _inventoryEventRepository;
        private IInventoryWriteRepository _inventoryWriteRepository;

		public InventoryCommandHandler(
            IInventoryEventRepository inventoryEventRepository,
            IInventoryWriteRepository inventoryWriteRepository
        )
        {
            _inventoryEventRepository = inventoryEventRepository;
            _inventoryWriteRepository = inventoryWriteRepository;
        }

        private async Task _HandleUpdate(AInventoryItemEvent command)
        {
			InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, command.AggregateId);

			// ToDo: Cache aggregate and attempt to update rather than regenerate
			InventoryItemAggregate iia = (InventoryItemAggregate)await iie.ModelAsync();

			command.ApplyEventData(iia);

			await Task.WhenAll(
				iie.AppendEventAsync((IModelEvent<Guid>)command),
				_inventoryWriteRepository.UpdateAsync(
					command.AggregateId,
					new InventoryItemDto(iia)
			));
		}

        public async Task Handle(ActivateInventoryItem command)
		{
            await _HandleUpdate(command);
		}

		public async Task Handle(CreateInventoryItem command)
        {
			InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, command.AggregateId);

            // ToDo: Add check for duplicate create of inventory item?

			var eventData = ((CreateInventoryItemData)command.EventData).InventoryItemData;

            await Task.WhenAll(
                iie.AppendEventAsync((IModelEvent<Guid>)command),
                _inventoryWriteRepository.AppendAsync(
                    command.AggregateId,
                    new InventoryItemDto()
                    {
                        Id = command.AggregateId,
                        LastEventTimestamp = command.Timestamp,
                        Name = eventData.Name,
                        IsActive = eventData.IsActive,
                        Count = eventData.Count,
                        Note = eventData.Note
                    }
            ));
		}

		public async Task Handle(DeleteInventoryItem command)
		{
            InventoryItemEvents iie = new InventoryItemEvents(_inventoryEventRepository, command.AggregateId);

            // ToDo: Cache aggregate and attempt to update rather than regenerate
			InventoryItemAggregate iia = (InventoryItemAggregate)await iie.ModelAsync();

			await Task.WhenAll(
                iie.AppendEventAsync((IModelEvent<Guid>)command)
                ,_inventoryWriteRepository.DeleteAsync(command.AggregateId)
			);
		}

        public async Task Handle(DeactivateInventoryItem command)
        {
            await _HandleUpdate(command);
		}

        public async Task Handle(DecreaseInventoryItemCount command)
        {
			await _HandleUpdate(command);
		}

        public async Task Handle(IncreaseInventoryItemCount command)
        {
			await _HandleUpdate(command);
		}

        public async Task Handle(SetInventoryItemCount command)
        {
			await _HandleUpdate(command);
		}

        public async Task Handle(SetInventoryItemName command)
        {
			await _HandleUpdate(command);
		}

        public async Task Handle(SetInventoryItemNote command)
        {
			await _HandleUpdate(command);
		}

		public async Task Handle(UpdateInventoryItem command)
		{
			await _HandleUpdate(command);
		}
	}
}
