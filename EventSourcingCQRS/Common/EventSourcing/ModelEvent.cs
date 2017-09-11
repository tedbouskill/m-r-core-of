using System;

using Common.EventSourcing.Interfaces;

namespace Common.EventSourcing
{
	/// <summary>
	/// Represents an event that affects a models state
	/// </summary>
    public class ModelEvent<KeyT>: IModelEvent<KeyT>
	{
		public KeyT AggregateKey { get; set; }

		public DateTime Timestamp { get; set; }

		public string EventName { get; set; }

        public IModelEventData<KeyT> EventData { get; set; }
	}
}
