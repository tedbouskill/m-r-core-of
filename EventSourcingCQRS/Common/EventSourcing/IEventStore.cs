using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.EventSourcing
{
    public interface IEventStore<KeyType>
    {
        Task<IEnumerable<EventModel>> AggregateEvents(KeyType aggregateKey);

        Task AppendEvent(KeyType aggregateKey, EventModel eventModel);
    }
}
