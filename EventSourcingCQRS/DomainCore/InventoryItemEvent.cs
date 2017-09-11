using System;

using Common.EventSourcing.Interfaces;

namespace DomainCore
{
    public class InventoryItemEvent : IModelEvent<Guid>
    {
        public Guid AggregateKey { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventName { get; set; }
        public IModelEventData<Guid> EventData { get; set; }
    }
}
