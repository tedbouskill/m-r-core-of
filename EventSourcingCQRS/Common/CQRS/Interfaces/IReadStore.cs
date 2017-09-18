using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
    /// <summary>
    /// Used as a decorator for reading from a data store
    /// </summary>
    public interface IReadStore<IdType, ModelType>
    {
        Task<int> ModelsCountAsync();

        Task<IEnumerable<ModelType>> AllAsync();

        Task<ModelType> ModelAsync(IdType id);
    }
}
