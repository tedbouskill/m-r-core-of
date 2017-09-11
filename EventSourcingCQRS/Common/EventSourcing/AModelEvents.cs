using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Common.EventSourcing.Interfaces;

namespace Common.EventSourcing
{
    /// <summary>
    /// Represents an aggregate of Event Source items for an Aggregate Model
    /// </summary>
    public abstract class AModelEvents<KeyT> : IModelEvents<KeyT>
    {
        /// <summary>
        /// An event store capable of storing a set of events using one key in the order events are added
        /// </summary>
        private IEventStore<KeyT> _eventStore;

        private KeyT _aggregateKey;

        public AModelEvents(IEventStore<KeyT> eventStore, KeyT aggregateKey)
        {
            _aggregateKey = aggregateKey;
            _eventStore = eventStore;
        }

        public async Task<IEnumerable<IModelEvent<KeyT>>> EventsAsync()
        {
            return await _eventStore.EventsAsync(_aggregateKey);
        }

        /// <summary>
        /// Appends the event model
        /// </summary>
        /// <returns>The aggregate model</returns>
        /// <param name="eventModel">Event source model.</param>
        public async Task<int> AppendEventAsync(IModelEvent<KeyT> eventModel)
        {
            return await _eventStore.AppendEventAsync(eventModel);
        }

        /// <summary>
        /// The aggregate model for all the events
        /// </summary>
        /// <returns>The aggregate model</returns>
        public abstract Task<ModelAggregate<KeyT>> ModelAsync();
    }
}
