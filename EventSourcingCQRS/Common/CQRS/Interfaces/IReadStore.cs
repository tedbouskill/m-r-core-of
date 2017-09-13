using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
    public interface IReadStore<IdType, ModelType>
    {
        Task<IEnumerable<ModelType>> AllAsync();

        Task<ModelType> ModelAsync(IdType id);
    }
}
