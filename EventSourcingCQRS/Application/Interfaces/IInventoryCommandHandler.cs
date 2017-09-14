using Application.Commands;
using Common.CQRS.Interfaces;

namespace Application.Interfaces
{
    public interface IInventoryCommandHandler:
        ICommandHandler<CreateInventoryItem>,
        ICommandHandler<DeleteInventoryItem>,
        ICommandHandler<UpdateInventoryItem>
    {
    }
}
