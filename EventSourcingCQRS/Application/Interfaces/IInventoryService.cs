using System;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInventoryService
    {
        /* ReadModel  */
        // Get all
        Task<IEnumerable<DomainCore.InventoryItem>> InventoryAsync();

        // Get one
        Task<DomainCore.InventoryItem> GetItemAsync(Guid id);

        /* Commands */
        // Create Item
        Task PostItemAsync(DomainCore.InventoryItem item);

        // Update Full Item
		Task PutItemAsync(DomainCore.InventoryItem item);

        // Delete Item
        Task DeleteItemAsync(Guid id);

		// Edit Item
		Task PatchItemCountAsync(Guid id, int count, string reason);
		Task PatchItemNameAsync(Guid id, string name, string reason);
		Task PatchItemNoteAsync(Guid id, string note, string reason);

        // Increase Item count
        Task IncreaseInventory(Guid id, uint amount, string reason);

		// Decrease Item count
		Task DecreaseInventory(Guid id, uint amount, string reason);

		// Activate Item
		Task ActivateItem(Guid id, string reason);

        // Deactivate Item
        Task DisableItem(Guid id, string reason);
	}
}
