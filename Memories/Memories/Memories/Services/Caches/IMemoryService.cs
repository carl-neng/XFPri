using System.Collections.Generic;

namespace Memories.Services.Caches
{
    public interface IMemoryService
    {
        string BaseUrl { get; set; }
        string Token { get; set; }
        Dictionary<string, string> Permissions { get; set; }
    }
}
