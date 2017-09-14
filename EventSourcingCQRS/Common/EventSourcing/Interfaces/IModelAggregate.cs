using System;

namespace Common.EventSourcing.Interfaces
{
	/// <summary>
	/// Represents the aggregate of the model's event data changes
	/// </summary>
	public interface IModelAggregate<IdT>
	{
        /// <summary>
        /// The aggregate Id represents the model events and the active model
        /// </summary>
        /// <value>The aggregate identifier.</value>
		IdT AggregateId { get; set; }

		int EventModelRow { get; set; }

		DateTime LastEventTimestamp { get; set; }
	}
}
