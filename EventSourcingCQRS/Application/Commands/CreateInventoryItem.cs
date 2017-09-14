using System;
using Application.EventData;
using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;
using Newtonsoft.Json;

namespace Application.Commands
{
    public class CreateInventoryItem : AInventoryItemEvent, ICommand
    {
        public CreateInventoryItem() {}

        public CreateInventoryItem(InventoryItemDto inventoryItem)
        {
            AggregateId = inventoryItem.Id;
            Timestamp = inventoryItem.LastEventTimestamp;
            EventName = "CreateInventoryItem";
            EventData = new CreateInventoryItemData(inventoryItem);
        }

        public override string DataAsJson
		{
			get
			{
				var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
				return JsonConvert.SerializeObject(this.EventData, settings);
			}
		}

        public override void ApplyEventData(IModelAggregate<Guid> model)
        {
            throw new NotImplementedException();
        }
    }
}
