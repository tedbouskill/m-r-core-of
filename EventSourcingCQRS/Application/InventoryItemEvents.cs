using System;
using System.Threading.Tasks;

using Common.EventSourcing;

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

        public override Task<ModelAggregate<Guid>> ModelAsync()
        {
            throw new NotImplementedException();
        }
	}
}
