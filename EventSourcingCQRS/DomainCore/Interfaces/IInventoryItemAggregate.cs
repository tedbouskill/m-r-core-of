using System;

namespace DomainCore.Interfaces
{
    public interface IInventoryItemAggregate : Common.EventSourcing.Interfaces.IModelAggregate<Guid>
    {
    }
}
