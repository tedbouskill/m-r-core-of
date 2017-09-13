using System;

namespace Common.EventSourcing.Interfaces
{
	/// <summary>
	/// Represents the aggregate of the model's event data changes
	/// </summary>
	public interface IModelAggregate<IdT>
	{
		IdT AggregateId { get; set; }

		int EventModelRow { get; set; }

		DateTime LastEventTimestamp { get; set; }
	}
}
