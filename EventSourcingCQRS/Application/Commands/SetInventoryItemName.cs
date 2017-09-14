using System;

using Application.EventData;
using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;
using Newtonsoft.Json;

namespace Application.Commands
{
    public class SetInventoryItemName : AInventoryItemEvent, ICommand
    {
        public SetInventoryItemName() { }

        public SetInventoryItemName(Guid id, SetInventoryItemNameData eventData)
        {
            AggregateId = id;
            Timestamp = DateTime.UtcNow;
            EventName = "SetInventoryItemName";
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
            ((InventoryItemAggregate)model).Name = ((SetInventoryItemNameData)EventData).Name;
			((InventoryItemAggregate)model).LastEventTimestamp = Timestamp;
		}
    }
}
