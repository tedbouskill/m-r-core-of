using System;
namespace DomainCore
{
    public class InventoryItemData : Interfaces.IInventoryItemData
    {
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public int Count { get; set; }
		public string Note { get; set; }
	}
}
