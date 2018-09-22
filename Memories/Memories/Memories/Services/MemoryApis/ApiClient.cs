using Memories.Services.Caches;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Memories.Services.MemoryApis.Dtos;

namespace Memories.Services.MemoryApis
{
    public class ApiClient: IApiClient
    {
        protected HttpClient Client;
        private readonly IMemoryService _memService;

        public ApiClient(IMemoryService memService)
        {
            _memService = memService;
        }

        #region External Methods
        public async Task<BaseApiOutput<T>>
           PostAsync<T>(string apiName, bool tokenRequired = true)
        {
            return await HelperAsync<T, string>(apiName, null, PostHelperAsync, tokenRequired);
        }

        public async Task<BaseApiOutput<T>>
              PostAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true)
        {
            return await HelperAsync<T, I>(apiName, apiInput, PostHelperAsync, tokenRequired);
        }

        public async Task<BaseApiOutput<T>>
           PutAsync<T>(string apiName, bool tokenRequired = true)
        {
            return await HelperAsync<T, string>(apiName, null, PutHelperAsync, tokenRequired);
        }

        public async Task<BaseApiOutput<T>>
              PutAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true)
        {
            return await HelperAsync<T, I>(apiName, apiInput, PutHelperAsync, tokenRequired);
        }


        public async Task<BaseApiOutput<T>>
           GetAsync<T>(string apiName, bool tokenRequired = true)
        {
            return await HelperAsync<T, string>(apiName, null, GetHelperAsync, tokenRequired);
        }

        public async Task<BaseApiOutput<T>>
              GetAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true)
        {
            return await HelperAsync<T, I>(apiName, apiInput, GetHelperAsync, tokenRequired);
        }

        public async Task<BaseApiOutput<T>>
         DeleteAsync<T>(string apiName, bool tokenRequired = true)
        {
            return await HelperAsync<T, string>(apiName, null, DeleteHelperAsync, tokenRequired);
        }

        public async Task<BaseApiOutput<T>>
              DeleteAsync<T, I>(string apiName, I apiInput, bool tokenRequired = true)
        {
            return await HelperAsync<T, I>(apiName, apiInput, DeleteHelperAsync, tokenRequired);
        }

        #endregion  


     

        


        #region Helpers


        private string BaseUrl => _memService.BaseUrl;

        private bool InitClient()
        {
            if (Client == null && !string.IsNullOrEmpty(BaseUrl))
            {
                Client = new HttpClient {BaseAddress = new Uri(BaseUrl)};
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            return Client != null;
        }


        private async Task<HttpResponseMessage> PostHelperAsync<I>(string apiName, I apiInput)
        {
            var uri = new Uri($"{BaseUrl}/{apiName}");
            var json = apiInput == null ? "" : JsonConvert.SerializeObject(apiInput);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(uri, content);

            return response;
        }

        private async Task<HttpResponseMessage> PutHelperAsync<I>(string apiName, I apiInput)
        {
            var uri = new Uri($"{BaseUrl}/{apiName}");
            var json = apiInput == null ? "" : JsonConvert.SerializeObject(apiInput);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PutAsync(uri, content);

            return response;
        }

        private async Task<HttpResponseMessage> GetHelperAsync<I>(string apiName, I apiInput)
        {
            var query = apiInput != null ? "?" + apiInput.ToQueryString() : "";

            var uri = new Uri($"{BaseUrl}/{apiName}{query}");

            var response = await Client.GetAsync(uri);

            return response;
        }

        private async Task<HttpResponseMessage> DeleteHelperAsync<I>(string apiName, I apiInput)
        {
            var query = apiInput != null ? "?" + apiInput.ToQueryString() : "";

            var uri = new Uri($"{BaseUrl}/{apiName}{query}");

            var response = await Client.DeleteAsync(uri);

            return response;
        }




        private async Task<BaseApiOutput<T>>
            HelperAsync<T, I>(string apiName, I apiInput, Func<string, I, Task<HttpResponseMessage>> httpMethodAsync, bool tokenRequired = true)
        {
            try
            {
                if (!InitClient()) return null;


                if (tokenRequired)
                {
                    var token = _memService.Token;
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                if (httpMethodAsync != null)
                {
                    var response = await httpMethodAsync.Invoke(apiName, apiInput);

                    if (response.IsSuccessStatusCode)
                    {
                        var rescontent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BaseApiOutput<T>>(rescontent);

                        return result;
                    }
                    else
                    {
                    
                        var rescontent = await response.Content.ReadAsStringAsync();
                        var resultErrors = JsonConvert.DeserializeObject<BaseApiOutput<T>>(rescontent);

                        return resultErrors;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        #endregion

    }

    public static class UrlHelpers
    {
        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType != null && (valueElemType.IsPrimitive || valueElemType == typeof(string)))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, (enumerable ?? throw new InvalidOperationException()).Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}
