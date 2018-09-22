using Akavache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Memories.Services.Caches
{
    public class MemCache : BaseCache, IMemCache
    {
        public MemCache():base(Akavache.BlobCache.InMemory)
        {
           
        }
        
    }
}
