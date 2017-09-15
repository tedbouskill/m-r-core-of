using System;
using Common.EventSourcing.Interfaces;
using DomainCore.Interfaces;

namespace DomainCore
{
    /// <summary>
    /// Represents the changes to an inventory item based on an event
    /// </summary>
    public abstract class AInventoryItemEvent : IInventoryItemEvent
	{
		public Guid AggregateId { get; set; }
		public DateTime Timestamp { get; set; }
		public string EventName { get; set; }

		public object EventData { get; set; }

		public abstract string DataAsJson { get; }

		public abstract void ApplyEventData(IModelAggregate<Guid> model);
	}
}
