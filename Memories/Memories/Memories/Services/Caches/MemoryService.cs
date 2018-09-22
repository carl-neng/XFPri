using System.Collections.Generic;

namespace Memories.Services.Caches
{
    public class MemoryService : IMemoryService
    {
        private string _baseUrl;
        public string BaseUrl
        {
            get => string.IsNullOrEmpty(_baseUrl) ? 
                "http://192.168.2.22:21021" : _baseUrl;
            set => _baseUrl = value;
        }

        public string Token { get; set; }
       
        public Dictionary<string, string> Permissions { get; set; }
    }
}
