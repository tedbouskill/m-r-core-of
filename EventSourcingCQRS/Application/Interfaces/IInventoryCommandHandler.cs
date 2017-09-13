using Common.CQRS.Interfaces;
using DomainCore.Commands;

namespace Application.Interfaces
{
    public interface IInventoryCommandHandler:
        ICommandHandler<CreateInventoryItem>,
        ICommandHandler<DeleteInventoryItem>,
        ICommandHandler<UpdateInventoryItem>
    {
    }
}
