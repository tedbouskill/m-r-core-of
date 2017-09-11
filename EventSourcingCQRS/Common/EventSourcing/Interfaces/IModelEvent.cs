using System;
using System.Threading.Tasks;

using Common.EventSourcing;

namespace Common.EventSourcing.Interfaces
{
	/// <summary>
	/// Represents an event that affects a models state
	/// </summary>
    public interface IModelEvent<KeyT>
    {
		KeyT AggregateKey { get; set; }

		DateTime Timestamp { get; set; }

		string EventName { get; set; }

        IModelEventData<KeyT> EventData { get; set; }
	}
}
