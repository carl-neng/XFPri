using Akavache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Memories.Services.Caches
{
    public class SecureCache : BaseCache, ISecureCache
    {
        public SecureCache():base(Akavache.BlobCache.Secure)
        {
           
        }
        
    }
}
