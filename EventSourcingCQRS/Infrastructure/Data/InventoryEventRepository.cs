using System;
using System.Collections.Generic;
using Common.EventSourcing;

using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data
{
    public class InventoryEventRepository : IInventoryEventRepository
    {
		private readonly InventoryDbContext _dbContext;

		public InventoryEventRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        IEnumerable<EventModel> IEventStore<Guid>.AggregateEvents(Guid aggregateKey)
        {
            throw new NotImplementedException();
        }

        void IEventStore<Guid>.AppendEvent(Guid aggregateKey, EventModel eventModel)
        {
            throw new NotImplementedException();
        }
    }
}
