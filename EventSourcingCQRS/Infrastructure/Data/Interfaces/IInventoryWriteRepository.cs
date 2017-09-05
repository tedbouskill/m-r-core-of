using System;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryWriteRepository : Common.CQRS.IWriteStore<Guid, DomainCore.InventoryItem>
    {
    }
}
