using System;
namespace DomainCore.Interfaces
{
    /// <summary>
    /// A full inventory item
    /// </summary>
    public interface IInventoryItem : IInventoryItemData
    {
		/// <summary>
		/// Unique Id representing a SKU at a location
		/// </summary>
		/// <value>The identifier.</value>
		Guid Id { get; set; }

        /// <summary>
        /// Used to track the last event applied to the inventory item
        /// </summary>
        /// <value>The last event timestamp.</value>
		DateTime LastEventTimestamp { get; set; }
	}
}
