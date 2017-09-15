using System;

using Application.EventData;
using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;
using Newtonsoft.Json;

namespace Application.Commands
{
    public class SetInventoryItemCount : AInventoryItemEvent, ICommand
    {
        public SetInventoryItemCount() { }

        public SetInventoryItemCount(Guid id, SetInventoryItemCountData eventData)
        {
            AggregateId = id;
            Timestamp = DateTime.UtcNow;
            EventName = "SetInventoryItemCount";
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
            ((InventoryItemAggregate)model).Count = ((SetInventoryItemCountData)EventData).Count;
			((InventoryItemAggregate)model).LastEventTimestamp = Timestamp;
		}
    }
}
