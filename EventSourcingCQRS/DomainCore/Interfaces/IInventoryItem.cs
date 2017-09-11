using System;
namespace DomainCore.Interfaces
{
    public interface IInventoryItem : IInventoryItemData
    {
		/// <summary>
		/// Unique Id representing a SKU and a location
		/// </summary>
		/// <value>The identifier.</value>
		Guid Id { get; set; }

		DateTime LastEventTimestamp { get; set; }
	}
}
