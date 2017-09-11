using System;
namespace DomainCore
{
	/// <summary>
	/// Mutable inventory model data that can be changed by user events
	/// </summary>
	public class InventoryItemData : Interfaces.IInventoryItemData
    {
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public int Count { get; set; }
		public string Note { get; set; }
	}
}
