using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Common.EventSourcing;

namespace Common.EventSourcing.Interfaces
{
    /// <summary>
    /// Methods that are required for model event management
    /// </summary>
    public interface IModelEvents<IdT>
    {
        Task<IEnumerable<IModelEvent<IdT>>> EventsAsync();

        Task<int> AppendEventAsync(IModelEvent<IdT> modelEvent);

        Task<ModelAggregate<IdT>> ModelAsync();
	}
}
