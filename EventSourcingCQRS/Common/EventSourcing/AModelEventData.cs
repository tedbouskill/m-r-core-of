using System;

using Common.EventSourcing.Interfaces;

namespace Common.EventSourcing
{
    /// <summary>
    /// Data used to change a models state in an event
    /// </summary>
    public abstract class AModelEventData<KeyT> : IModelEventData<KeyT>
    {
        /// <summary>
        /// This object should know how to change the state of the model
        /// </summary>
        /// <param name="model">The model that needs to be changed</param>
        public abstract void ApplyEventData(IModelAggregate<KeyT> model);
    }
}
