using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Common.EventSourcing;

namespace Common.EventSourcing.Interfaces
{
    /// <summary>
    /// Methods that are required for model event management
    /// </summary>
    public interface IModelEvents<KeyT>
    {
        Task<IEnumerable<IModelEvent<KeyT>>> EventsAsync();

        Task<int> AppendEventAsync(IModelEvent<KeyT> modelEvent);

        Task<ModelAggregate<KeyT>> ModelAsync();
	}
}
