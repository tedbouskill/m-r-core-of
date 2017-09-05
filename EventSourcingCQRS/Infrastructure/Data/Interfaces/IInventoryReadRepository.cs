using System;

namespace Infrastructure.Data.Interfaces
{
    public interface IInventoryReadRepository : Common.CQRS.IReadStore<Guid, DomainCore.InventoryItem>
    {
    }
}
