using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Common.EventSourcing;

namespace Common.EventSourcing.Interfaces
{
    /// <summary>
    /// Stores model events for all models
    /// </summary>
    public interface IEventStore<IdT>
    {
        Task<int> EventsCountAsync(IdT aggregateId);

		//Task<IEnumerable<IdT>> AllIdsSync();

		Task<IEnumerable<IModelEvent<IdT>>> EventsAsync(IdT aggregateId);

        Task<int> AppendEventAsync(IModelEvent<IdT> eventModel);
    }
}
