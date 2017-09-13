using System;

namespace DomainCore.Interfaces
{
    public interface IInventoryItemEvent : Common.EventSourcing.Interfaces.IModelEvent<Guid>
    {
    }
}
