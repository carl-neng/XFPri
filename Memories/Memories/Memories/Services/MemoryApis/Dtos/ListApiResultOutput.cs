using System.Collections.Generic;

namespace Memories.Services.MemoryApis.Dtos
{
    public class ListApiResultOutput<T>
    {
        public List<T> Items { get; set; }
    }
}
