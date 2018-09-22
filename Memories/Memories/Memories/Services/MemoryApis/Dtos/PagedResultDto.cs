using System.Collections.Generic;

namespace Memories.Services.MemoryApis.Dtos
{
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
