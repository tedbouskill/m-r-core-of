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

        IModelEventData<IdT> EventData { get; }
	}
}
