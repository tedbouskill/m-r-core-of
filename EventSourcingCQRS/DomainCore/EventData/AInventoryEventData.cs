using System;

using Common.EventSourcing.Interfaces;

namespace DomainCore.EventData
{
    /// <summary>
    /// Base class for all Inventory events
    /// </summary>
	public abstract class AInventoryEventData : IModelEventData<Guid>
    {
        public abstract void ApplyEventData(IModelAggregate<Guid> model);
    }
}
