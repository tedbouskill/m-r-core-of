using System;

namespace Common.EventSourcing.Interfaces
{
    /// <summary>
    /// To represent the data used to change the model in the event
    /// </summary>
    public interface IModelEventData<KeyT>
    {
        /// <summary>
        /// Will apply the change to the aggregate model
        /// </summary>
        /// <param name="model">Model.</param>
        void ApplyEventData(IModelAggregate<KeyT> model);
    }
}
