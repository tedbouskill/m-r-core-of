using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Infrastructure.Data.Interfaces;
using DomainCore;

using Common.EventSourcing;
using Common.EventSourcing.Interfaces;

namespace Infrastructure.Data
{
    public class InventoryEventRepository : IInventoryEventRepository
    {
		private readonly InventoryDbContext _dbContext;

		public InventoryEventRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IModelEvent<Guid>>> EventsAsync(Guid aggregateId)
		{
			var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
			
            // Note: This doesn't execute the query so there is no impact on memory
            IQueryable<InventoryItemEventDto> items = _dbContext.InventoryEventItems.AsQueryable();

            return await items.Where(i => i.AggregateId == aggregateId)
                              .Select(i =>  
                                      new InventoryItemEvent()
                                    {
                                        AggregateId = i.AggregateId,
                                        Timestamp = i.Timestamp,
                                        EventName = i.EventName,
                                        EventData = JsonConvert.DeserializeObject<IModelEventData<Guid>>(i.EventObjJson, settings)
                                    }
                                 )
                              .ToListAsync();
		}

        public async Task<int> AppendEventAsync(IModelEvent<Guid> eventModel)
        {
			var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

		    await _dbContext.InventoryEventItems.AddAsync(
                    new InventoryItemEventDto() {
                        AggregateId = eventModel.AggregateId,
                        Timestamp = eventModel.Timestamp,
                        EventName = eventModel.EventName,
                        EventObjJson = JsonConvert.SerializeObject(eventModel.EventData, typeof(object), settings)
                    }
                );

            await _dbContext.SaveChangesAsync();

            return _dbContext.InventoryEventItems.Select(i => i.AggregateId == eventModel.AggregateId).Count();
        }

        public Task<int> EventsCountAsync(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
