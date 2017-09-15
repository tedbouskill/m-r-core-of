using System;

using DomainCore.Interfaces;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryEventRepository : Common.EventSourcing.Interfaces.IEventStore<Guid>
    {
    }
}
