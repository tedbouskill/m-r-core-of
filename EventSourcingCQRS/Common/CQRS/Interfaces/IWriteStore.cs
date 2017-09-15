using System;
using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
	/// <summary>
	/// Used as a decorator for writing to a data store
	/// </summary>
	public interface IWriteStore<IdType, ModelType>
    {
        Task AppendAsync(IdType id, ModelType model);
        Task DeleteAsync(IdType id);
        Task UpdateAsync(IdType id, ModelType model);
	}
}
