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
        Task<int> EventsCountAsync(KeyT aggregateKey);

		//Task<IEnumerable<KeyT>> AllKeysSync();

		Task<IEnumerable<IModelEvent<KeyT>>> EventsAsync(KeyT aggregateKey);

        Task<int> AppendEventAsync(IModelEvent<KeyT> eventModel);
    }
}
