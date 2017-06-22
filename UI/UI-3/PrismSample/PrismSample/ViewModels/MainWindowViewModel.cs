using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismSample.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            eventAggregator.GetEvent<ClientNavigationRequestedToEdit>().Subscribe(ClientNavigationRequestedToEdit);
            eventAggregator.GetEvent<ClientNavigationRequestedToDetail>().Subscribe(ClientNavigationRequestedToDetail);
            eventAggregator.GetEvent<ClientNavigationRequestedToList>().Subscribe(ClientNavigationRequestedToList);
        }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri );
        }

        private void ClientNavigationRequestedToEdit(string obj)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("ClientId", obj);

            _regionManager.RequestNavigate("ContentRegion", "ViewSaveClient", navParameters);
        }

        private void ClientNavigationRequestedToDetail(string obj)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("ClientId", obj);

            _regionManager.RequestNavigate("ContentRegion", "ViewDetailClient", navParameters);
        }

        private void ClientNavigationRequestedToList(string obj)
        {
            _regionManager.RequestNavigate("ContentRegion", "ViewListClient");
        }
    }
}
