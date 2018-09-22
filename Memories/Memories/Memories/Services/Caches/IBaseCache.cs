using System.Threading.Tasks;

namespace Memories.Services.Caches
{
    public interface IBaseCache
    {
        Task<T> GetObjectAsync<T>(string key);
        Task InsertObjectAsync<T>(string key, T value);
        Task RemoveObjectAsync(string key);
        Task UpdateObjectAsync<T>(string key, T value);
    }
}
