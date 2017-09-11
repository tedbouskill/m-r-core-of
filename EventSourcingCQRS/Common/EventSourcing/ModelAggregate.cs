using System;

using Common.EventSourcing.Interfaces;

namespace Common.EventSourcing
{
	/// <summary>
	/// Represents the aggregate of the model's event data changes
	/// </summary>
    public class ModelAggregate<KeyT> : IModelAggregate<KeyT>
	{
		public KeyT AggregateKey { get; set; }

		public int EventModelRow { get; set; }

		public DateTime LastEventTimestamp { get; set; }
	}
}

