using System;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryEventRepository : Common.EventSourcing.IEventStore<Guid>
    {
    }
}
