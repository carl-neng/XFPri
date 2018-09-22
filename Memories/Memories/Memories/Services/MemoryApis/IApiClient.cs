using System.Threading.Tasks;
using Memories.Services.MemoryApis.Dtos;

namespace Memories.Services.MemoryApis
{
    public interface IApiClient
    {
        Task<BaseApiOutput<T>>
            PostAsync<T>(string apiName, bool tokenRequired = true);

        Task<BaseApiOutput<T>>
            PostAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true);

        Task<BaseApiOutput<T>>
            PutAsync<T>(string apiName, bool tokenRequired = true);

        Task<BaseApiOutput<T>>
            PutAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true);


        Task<BaseApiOutput<T>>
            GetAsync<T>(string apiName, bool tokenRequired = true);

        Task<BaseApiOutput<T>>
            GetAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true);

        Task<BaseApiOutput<T>>
            DeleteAsync<T>(string apiName, bool tokenRequired = true);

        Task<BaseApiOutput<T>>
            DeleteAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true);
    }
}
