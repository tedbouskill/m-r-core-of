using Application.Commands;
using Common.CQRS.Interfaces;

namespace Application.Interfaces
{
    public interface IInventoryCommandHandler:
    ICommandHandler<ActivateInventoryItem>,
    ICommandHandler<CreateInventoryItem>,
    ICommandHandler<DeactivateInventoryItem>,
    ICommandHandler<DecreaseInventoryItemCount>,
	ICommandHandler<DeleteInventoryItem>,
    ICommandHandler<IncreaseInventoryItemCount>,
    ICommandHandler<SetInventoryItemCount>,
    ICommandHandler<SetInventoryItemName>,
    ICommandHandler<SetInventoryItemNote>,
	ICommandHandler<UpdateInventoryItem>
    {
    }
}
