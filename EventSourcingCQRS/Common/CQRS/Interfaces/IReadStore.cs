using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
    public interface IReadStore<KeyType, ModelType>
    {
        Task<IEnumerable<ModelType>> AllAsync();

        Task<ModelType> ModelAsync(KeyType key);
    }
}
