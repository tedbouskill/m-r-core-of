using Application.Models;
using DomainCore;
using DomainCore.Interfaces;

namespace Application.EventData
{
    public class UpdateInventoryItemData : IInventoryItemEventData
    {
        public InventoryItemData InventoryItemData { get; set; }

        public UpdateInventoryItemData()
        {
        }

        public UpdateInventoryItemData(InventoryItemDto inventoryItem)
        {
            InventoryItemData = new InventoryItemData()
            {
                Name = inventoryItem.Name,
                IsActive = inventoryItem.IsActive,
                Count = inventoryItem.Count,
                Note = inventoryItem.Note
            };
        }
    }
}
