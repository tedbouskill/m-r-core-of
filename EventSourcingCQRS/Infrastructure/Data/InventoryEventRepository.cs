using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

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

        Task<IEnumerable<EventModel>> IEventStore<Guid>.AggregateEvents(Guid aggregateKey)
        {
            throw new NotImplementedException();
        }

        public async Task AppendEvent(Guid aggregateKey, EventModel eventModel)
        {
			_dbContext.Add(eventModel);

			await _dbContext.SaveChangesAsync();
        }
    }
}
