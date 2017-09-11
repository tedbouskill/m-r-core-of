using System;

namespace DomainCore.Interfaces
{
    /// <summary>
    /// The aggregate manages the event changes to the inventory item
    /// </summary>
    public interface IInventoryItemAggregate : Common.EventSourcing.Interfaces.IModelAggregate<Guid>
    {
    }
}
