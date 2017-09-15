using System;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryWriteRepository : Common.CQRS.Interfaces.IWriteStore<Guid, DomainCore.InventoryItemDto>
    {
    }
}
