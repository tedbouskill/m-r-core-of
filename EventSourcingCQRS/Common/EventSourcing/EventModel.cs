using System;
namespace Common.EventSourcing
{
    public class EventModel
    {
        public string Event { get; set; }

        public string EventObjTypeName { get; set; }

        public string EventObjJson { get; set; }
    }
}
