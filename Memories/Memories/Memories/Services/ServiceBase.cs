using System.Threading.Tasks;
using DryIoc;
using Memories.Datas;
using Memories.Services.Caches;
using Memories.Services.MemoryApis;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace Memories.Services
{
    public abstract class ServiceBase
    {
        protected INavigationService NavigationService { get; }
        protected ILocalCache LocalCacheService { get; }
        protected ISecureCache SecureCacheService { get; }
        protected IMemoryService MemService { get; set; }
        protected IPageDialogService DialogService { get; set; }
        protected IEventAggregator EventAggregator;
        protected IApiClient Client;
        protected IContainer Container;

        protected ServiceBase(IContainer container,
                           INavigationService navigationService,
                           ILocalCache localCahceService,
                           ISecureCache secureCacheService,
                           IMemoryService memService,
                           IEventAggregator eventAggregator,
                IPageDialogService dialogService)
        {
            Container = container;
            NavigationService = navigationService;
            LocalCacheService = localCahceService;
            SecureCacheService = secureCacheService;
            MemService = memService;
            EventAggregator = eventAggregator;
            DialogService = dialogService;

            Client = Container.Resolve<IApiClient>();
        }

        #region Access/Modify token in cache
        protected async Task<string> GetCacheTokenAsync(IBaseCache cacheService)
        {
            var token = await cacheService.GetObjectAsync<string>(CacheConst.Token);
            return token;
        }


        protected async Task SaveCacheTokenAsync(IBaseCache cacheService, string token)
        {
            await cacheService.UpdateObjectAsync(CacheConst.Token, token);
        }

        protected async Task ClearCacheTokenAsync(IBaseCache cacheService)
        {
            await cacheService.RemoveObjectAsync(CacheConst.Token);
        }

        #endregion

        #region Access/Modify Base Url in cache

        protected async Task SaveBaseUrlAsync(string baseUrl)
        {
            await LocalCacheService.UpdateObjectAsync(CacheConst.BaseUrl, baseUrl);
            MemService.BaseUrl = baseUrl;
        }

        protected async Task<string> GetBaseUrlAsync()
        {
            var baseUrl = await LocalCacheService.GetObjectAsync<string>(CacheConst.BaseUrl);
            if (baseUrl == null) return null;
            MemService.BaseUrl = baseUrl;
            return baseUrl;
        }
        #endregion

        //protected async Task<UserInfoModel> IsLoggedIn()
        //{
        //    var baseUrl = await GetBaseUrlAsync();
        //    if (string.IsNullOrEmpty(baseUrl)) return null;


        //    var token = await this.GetCacheTokenAsync(SecureCacheService);

        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        MemService.Token = token;

        //        var userDto = await Client.GetAsync<GeUserProfileApiOutput>(ApiNameConst.Url_Session_GetCurrentLoginInformations);

        //        if (userDto == null || !userDto.Success || userDto.Result == null || userDto.Result.User == null || userDto.Result.Company == null)
        //        {
        //            MemService.Token = "";
        //            await this.ClearCacheTokenAsync(SecureCacheService);
        //            return null;
        //        }


        //        var result = new UserInfoModel()
        //        {
        //            CompanyName = userDto.Result.Company.CompanyName,
        //            EmailAddress = userDto.Result.User.EmailAddress,
        //            FullName = userDto.Result.User.Surname + " " + userDto.Result.User.Name
        //        };

        //        MemService.UserInfo = result;
        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


    }
}
