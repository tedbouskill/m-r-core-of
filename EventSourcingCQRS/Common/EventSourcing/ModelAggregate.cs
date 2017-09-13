using System;

using Common.EventSourcing.Interfaces;

namespace Common.EventSourcing
{
	/// <summary>
	/// Represents the aggregate of the model's event data changes
	/// </summary>
    public class ModelAggregate<IdT> : IModelAggregate<IdT>
	{
		public IdT AggregateId { get; set; }

		public int EventModelRow { get; set; }

		public DateTime LastEventTimestamp { get; set; }
	}
}

