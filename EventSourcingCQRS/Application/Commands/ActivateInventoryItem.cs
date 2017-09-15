using System;

using Application.EventData;
using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;
using Newtonsoft.Json;

namespace Application.Commands
{
    public class ActivateInventoryItem : AInventoryItemEvent, ICommand
    {
        public ActivateInventoryItem() { }

        public ActivateInventoryItem(Guid id, SetInventoryItemActivation eventData)
        {
            AggregateId = id;
            Timestamp = DateTime.UtcNow;
            EventName = "ActivateInventoryItem";
            EventData = eventData;
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
            ((InventoryItemAggregate)model).IsActive = true;
            ((InventoryItemAggregate)model).LastEventTimestamp = Timestamp;
		}
    }
}
