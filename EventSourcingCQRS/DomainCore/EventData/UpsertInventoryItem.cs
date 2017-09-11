using System;

using Common.EventSourcing;
using Common.EventSourcing.Interfaces;

namespace DomainCore.EventData
{
    public class UpsertInventoryItem : AInventoryEventData
    {
        public InventoryItemData InventoryItemData { get; set; }

        public UpsertInventoryItem()
        {
        }

        public UpsertInventoryItem(InventoryItem inventoryItem)
        {
            InventoryItemData = new InventoryItemData()
                {
        		    Name = inventoryItem.Name,
        			IsActive = inventoryItem.IsActive,
        			Count = inventoryItem.Count,
        			Note = inventoryItem.Note
		        };
        }

        public override void ApplyEventData(IModelAggregate<Guid> model)
        {
			throw new NotImplementedException();
		}
    }
}
