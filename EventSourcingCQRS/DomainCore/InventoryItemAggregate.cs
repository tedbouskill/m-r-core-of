using System;
namespace DomainCore
{
	/// <summary>
	/// The aggregate manages the event changes to the inventory item
	/// </summary>
	public class InventoryItemAggregate : Common.EventSourcing.ModelAggregate<Guid>, Interfaces.IInventoryItemData
    {
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public int Count { get; set; }
		public string Note { get; set; }		
    }
}
