using System;

namespace Common.EventSourcing.Interfaces
{
	/// <summary>
	/// Represents an event that affects a models state
	/// </summary>
    public interface IModelEvent<IdT>
    {
		IdT AggregateId { get; }

		DateTime Timestamp { get; }

		string EventName { get; }

        object EventData { get; }

		/// <summary>
		/// Will apply the change to the aggregate model
		/// </summary>
		/// <param name="model">Model.</param>
		void ApplyEventData(IModelAggregate<IdT> model);
	}
}
