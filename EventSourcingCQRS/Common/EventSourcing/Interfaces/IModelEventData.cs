using System;

namespace Common.EventSourcing.Interfaces
{
    public interface IModelEventData<KeyT>
    {
        void ApplyEventData(IModelAggregate<KeyT> model);
    }
}
