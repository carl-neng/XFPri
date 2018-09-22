using Akavache;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Memories.Services.Caches
{
    public abstract class BaseCache: IBaseCache
    {
        protected IBlobCache BlobCache { get; set; }

        protected BaseCache(IBlobCache blobCache)
        {
            BlobCache = blobCache;
        }

        public async Task RemoveObjectAsync(string key)
        {
            if (BlobCache != null) await BlobCache.Invalidate(key);
        }

        public async Task<T> GetObjectAsync<T>(string key)
        {
            try
            {
                if (BlobCache != null) return await BlobCache.GetObject<T>(key);
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
            return default(T);
        }

        public async Task InsertObjectAsync<T>(string key, T value)
        {
            if (BlobCache != null) await BlobCache.InsertObject(key, value);
        }

        public async Task UpdateObjectAsync<T>(string key, T value)
        {
            //var data = GetObjectAsync<T>(key);
            if (BlobCache != null) await BlobCache.Invalidate(key);
            if (BlobCache != null) await BlobCache.InsertObject(key, value);
        }
    }
}
