using System;

using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore.EventData;
using DomainCore.Interfaces;

namespace DomainCore.Commands
{
    public class DeleteInventoryItem : IInventoryItemEvent, ICommand
    {
        public Guid AggregateId { get; }
        public DateTime Timestamp { get; }
        public string EventName { get; }
        public IModelEventData<Guid> EventData { get; }

        public DeleteInventoryItem(Guid id)
        {
            AggregateId = id;
            Timestamp = DateTime.UtcNow;
            EventName = "DeleteInventoryItem";
            EventData = null;
        }
    }
}
