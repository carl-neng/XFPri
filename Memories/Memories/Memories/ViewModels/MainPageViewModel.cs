using DryIoc;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Memories.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        protected MainPageViewModel(IContainer container, INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService) : 
            base(container, navigationService, eventAggregator, dialogService)
        {
            Title = "Main Page";
        }
    }
}
