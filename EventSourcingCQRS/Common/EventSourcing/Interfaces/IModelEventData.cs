using System;

namespace Common.EventSourcing.Interfaces
{
    /// <summary>
    /// To represent the data used to change the model in the event
    /// </summary>
    public interface IModelEventData<IdT>
    {
        /// <summary>
        /// Will apply the change to the aggregate model
        /// </summary>
        /// <param name="model">Model.</param>
        void ApplyEventData(IModelAggregate<IdT> model);
    }
}
