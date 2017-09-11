using System;

namespace DomainCore.Interfaces
{
    /// <summary>
    /// Mutable inventory model data that can be changed by user events
    /// </summary>
    public interface IInventoryItemData
    {
		string Name { get; set; }

		bool IsActive { get; set; }

		int Count { get; set; }

		string Note { get; set; }
	}
}
