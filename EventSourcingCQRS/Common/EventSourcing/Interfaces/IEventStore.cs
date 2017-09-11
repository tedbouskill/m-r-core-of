using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Common.EventSourcing;

namespace Common.EventSourcing.Interfaces
{
    /// <summary>
    /// Stores model events for all models
    /// </summary>
    public interface IEventStore<KeyT>
    {
        Task<int> EventsCount(KeyT aggregateKey);

		//Task<IEnumerable<KeyT>> Keys();

		Task<IEnumerable<IModelEvent<KeyT>>> Events(KeyT aggregateKey);

        Task<int> AppendEvent(IModelEvent<KeyT> eventModel);
    }
}
