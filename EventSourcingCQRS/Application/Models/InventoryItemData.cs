using DomainCore.Interfaces;

namespace Application.Models
{
	/// <summary>
	/// Mutable inventory model data that can be changed by user events
	/// </summary>
	public class InventoryItemData : IInventoryItemEventData
    {
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public int Count { get; set; }
		public string Note { get; set; }
	}
}
