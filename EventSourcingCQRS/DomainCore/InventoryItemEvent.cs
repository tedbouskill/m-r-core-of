using System;

using Common.EventSourcing.Interfaces;

namespace DomainCore
{
    /// <summary>
    /// Represents the changes to an inventory item based on an event
    /// </summary>
    public class InventoryItemEvent : IModelEvent<Guid>
    {
        public Guid AggregateKey { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventName { get; set; }
        public IModelEventData<Guid> EventData { get; set; }
    }
}
