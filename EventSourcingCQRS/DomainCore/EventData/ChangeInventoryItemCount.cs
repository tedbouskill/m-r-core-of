using System;

using Common.EventSourcing.Interfaces;

namespace DomainCore.EventData
{

    public class ChangeInventoryItemCount : AInventoryEventData
    {
        public int Count { get; set; }
	
        public override void ApplyEventData(IModelAggregate<Guid> model)
        {
            throw new NotImplementedException();
        }
    }
}
