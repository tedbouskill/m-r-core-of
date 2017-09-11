using System;

namespace DomainCore.Interfaces
{
    public interface IInventoryItemData
    {
		string Name { get; set; }

		bool IsActive { get; set; }

		int Count { get; set; }

		string Note { get; set; }
	}
}
