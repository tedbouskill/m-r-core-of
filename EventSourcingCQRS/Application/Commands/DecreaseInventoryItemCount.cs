using System;

using Application.EventData;
using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;
using Newtonsoft.Json;

namespace Application.Commands
{
    public class DecreaseInventoryItemCount : AInventoryItemEvent, ICommand
    {
        public DecreaseInventoryItemCount() { }

        public DecreaseInventoryItemCount(Guid id, AdjustInventoryItemCount eventData)
        {
            AggregateId = id;
            Timestamp = DateTime.UtcNow;
            EventName = "DecreaseInventoryItemCount";
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
            ((InventoryItemAggregate)model).Count -= (int)((AdjustInventoryItemCount)EventData).Delta;
			((InventoryItemAggregate)model).LastEventTimestamp = Timestamp;
		}
    }
}
