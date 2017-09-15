using System;

using Application.EventData;
using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;
using Newtonsoft.Json;

namespace Application.Commands
{
    public class SetInventoryItemNote : AInventoryItemEvent, ICommand
    {
        public SetInventoryItemNote() { }

        public SetInventoryItemNote(Guid id, SetInventoryItemNoteData eventData)
        {
            AggregateId = id;
            Timestamp = DateTime.UtcNow;
            EventName = "SetInventoryItemNote";
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
			((InventoryItemAggregate)model).Note = ((SetInventoryItemNoteData)EventData).Note;
			((InventoryItemAggregate)model).LastEventTimestamp = Timestamp;
		}
    }
}
