

namespace Memories.Services.MemoryApis.Dtos
{
    public class PagedAndFilteredInputDto
    {
        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }

        public string Filter { get; set; }
    }
}