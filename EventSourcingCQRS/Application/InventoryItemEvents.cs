using System;
using System.Threading.Tasks;

using Common.EventSourcing;
using DomainCore;

using Infrastructure.Data.Interfaces;

namespace Application
{
    /// <summary>
    /// Contain for inventory item events in an inventory item aggregate
    /// </summary>
    public class InventoryItemEvents : AModelEvents<Guid>
    {
        public InventoryItemEvents(IInventoryEventRepository eventStore, Guid aggregateId)
            : base(eventStore, aggregateId)
        {
        }

        public override async Task<ModelAggregate<Guid>> ModelAsync()
        {
            InventoryItemAggregate iia = new InventoryItemAggregate()
            {
                AggregateId = _aggregateId,
                EventModelRow = 1
            };

            foreach (AInventoryItemEvent eventItem in await _eventStore.EventsAsync(_aggregateId))
            {
                iia.EventModelRow++;
                eventItem.ApplyEventData(iia);
            }

            return iia;
        }
	}
}
