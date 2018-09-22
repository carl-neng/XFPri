using System.Collections.Generic;

namespace Memories.Services.MemoryApis.Dtos
{
    public class BaseApiOutput<T>
    {
        public T Result { get; set; }
        public string TargetUrl { get; set; }
        public bool Success { get; set; }
        public BaseApiErrorResultOutput Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
        public bool Abp { get; set; }
    }

    public class BaseApiErrorResultOutput
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public List<BaseApiValidationErrorResultOutput> ValidationErrors
        {
            get; set;
        }
    }

    public class BaseApiValidationErrorResultOutput
    {
        public string Message { get; set; }
        public string[] Members { get; set; }
    }
}

