using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Common.EventSourcing.Interfaces;

namespace Common.EventSourcing
{
    /// <summary>
    /// Represents an aggregate of Event Source items for an Aggregate Model
    /// </summary>
    public abstract class AModelEvents<IdT> : IModelEvents<IdT>
    {
        /// <summary>
        /// An event store capable of storing a set of events using one key in the order events are added
        /// </summary>
        protected IEventStore<IdT> _eventStore;

        protected IdT _aggregateId;

        public AModelEvents(IEventStore<IdT> eventStore, IdT aggregateId)
        {
            _aggregateId = aggregateId;
            _eventStore = eventStore;
        }

        public async Task<IEnumerable<IModelEvent<IdT>>> EventsAsync()
        {
            return await _eventStore.EventsAsync(_aggregateId);
        }

		public async Task<int> ModelEventsCountAsync(IdT id)
		{
            return await _eventStore.ModelEventsCountAsync(id);
		}

		/// <summary>
		/// Appends the event model
		/// </summary>
		/// <returns>The aggregate model</returns>
		/// <param name="eventModel">Event source model.</param>
		public async Task<int> AppendEventAsync(IModelEvent<IdT> eventModel)
        {
            return await _eventStore.AppendEventAsync(eventModel);
        }

        /// <summary>
        /// The aggregate model for all the events
        /// </summary>
        /// <returns>The aggregate model</returns>
        public abstract Task<ModelAggregate<IdT>> ModelAsync();
    }
}
