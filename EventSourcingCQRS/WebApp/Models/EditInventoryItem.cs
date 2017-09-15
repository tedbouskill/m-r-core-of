using Application.Models;
using DomainCore;

namespace WebApp.Models
{
    public class EditInventoryItem
    {
        public EditInventoryItem() {}

        public EditInventoryItem(InventoryItemDto inventoryItem)
        {
            Original = new InventoryItemData()
            {
                Name = inventoryItem.Name,
                IsActive = inventoryItem.IsActive,
                Count = inventoryItem.Count,
                Note = (inventoryItem.Note ?? "")
            };
            Editable = inventoryItem;
        }

        public InventoryItemData Original { get; set; }

        public InventoryItemDto Editable { get; set; }
    }
}
