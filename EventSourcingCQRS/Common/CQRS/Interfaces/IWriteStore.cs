using System;
using System.Threading.Tasks;

namespace Common.CQRS.Interfaces
{
    public interface IWriteStore<KeyType, ModelType>
    {
        Task Append(KeyType key, ModelType model);
        Task Delete(KeyType key);
        Task Update(KeyType key, ModelType model);
	}
}
