using System;
using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
    public interface IWriteStore<IdType, ModelType>
    {
        Task AppendAsync(IdType id, ModelType model);
        Task DeleteAsync(IdType id);
        Task UpdateAsync(IdType id, ModelType model);
	}
}
