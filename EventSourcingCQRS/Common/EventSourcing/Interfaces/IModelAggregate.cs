using System;

namespace Common.EventSourcing.Interfaces
{
	/// <summary>
	/// Represents the aggregate of the model's event data changes
	/// </summary>
	public interface IModelAggregate<KeyT>
	{
		KeyT AggregateKey { get; set; }

		int EventModelRow { get; set; }

		DateTime LastEventTimestamp { get; set; }
	}
}
