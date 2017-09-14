﻿using System;

using Common.CQRS.Interfaces;
using Common.EventSourcing.Interfaces;
using DomainCore;

namespace Application.Commands
{
    public class DeleteInventoryItem : AInventoryItemEvent, ICommand
    {
        public DeleteInventoryItem() {}

        public DeleteInventoryItem(Guid id)
        {
            AggregateId = id;
            Timestamp = DateTime.UtcNow;
            EventName = "DeleteInventoryItem";
            EventData = null;
        }

		public override string DataAsJson
		{
			get
			{
                return "{}";
			}
		}

        public override void ApplyEventData(IModelAggregate<Guid> model)
        {
            throw new NotImplementedException();
        }
    }
}
