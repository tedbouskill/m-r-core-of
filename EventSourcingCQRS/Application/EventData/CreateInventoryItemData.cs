using Application.Models;
using DomainCore.Interfaces;
using DomainCore;

namespace Application.EventData
{
    public class CreateInventoryItemData : IInventoryItemEventData
    {
        public InventoryItemData InventoryItemData { get; set; }

        public CreateInventoryItemData()
        {
        }

        public CreateInventoryItemData(InventoryItemDto inventoryItem)
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
