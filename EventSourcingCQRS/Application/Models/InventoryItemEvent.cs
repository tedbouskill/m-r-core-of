using System;

using Common.EventSourcing.Interfaces;
using DomainCore.Interfaces;
using Newtonsoft.Json;

namespace Application.Models
{	
    /// <summary>
	/// Represents the changes to an inventory item based on an event
	/// </summary>
	public class InventoryItemEvent : IInventoryItemEvent
    {
        public Guid AggregateId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventName { get; set; }

        public object EventData { get; set; }

        public InventoryItemEvent(){}

        public InventoryItemEvent(IInventoryItemEvent inventoryItemEvent)
        {
            AggregateId = inventoryItemEvent.AggregateId;
            Timestamp = inventoryItemEvent.Timestamp;
            EventName = inventoryItemEvent.EventName;
            EventData = inventoryItemEvent.EventData;
        }

        public void ApplyEventData(IModelAggregate<Guid> model)
        {
            throw new NotImplementedException();
        }

        public string DataAsJson
        {
            get
			{
				return JsonConvert.SerializeObject(this.EventData);
			}
		}
    }
}
