using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using DryIoc;
using Memories.Services.Caches;
using Prism.Events;
using Prism.Services;

namespace Memories.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IConfirmNavigationAsync
    {
        protected INavigationService NavigationService { get; }
        protected IPageDialogService DialogService { get; }
        protected IEventAggregator EventAggregator { get; }
        protected IContainer Container { get; set; }
        private IMemoryService MemoryService { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        protected ViewModelBase(IContainer container,
                             INavigationService navigationService,
                             IEventAggregator eventAggregator,
                             IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            EventAggregator = eventAggregator;
            Container = container;
            DialogService = dialogService;
            MemoryService = container.Resolve<IMemoryService>();
        }

        protected string BaseUrlInfo => MemoryService.BaseUrl;
        protected Dictionary<string, string> Permissions => MemoryService.Permissions;

        public string ProfileUrl => "https://image.flaticon.com/icons/png/512/146/146031.png";
      

       

        public virtual void Destroy()
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return Task.Run(() => true);
        }
    }
}
