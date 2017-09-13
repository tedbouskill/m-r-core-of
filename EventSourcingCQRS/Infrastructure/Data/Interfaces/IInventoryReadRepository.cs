using System;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryReadRepository : Common.CQRS.Interfaces.IReadStore<Guid, DomainCore.InventoryItemDto>
    {
    }
}
