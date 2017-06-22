using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismSample.Events;

namespace PrismSample.ViewModels
{
    public class ViewDetailClientViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;

        private ClientDataModel _item = null;
        public ClientDataModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        private void SetProperty(ref string _firstName, string value)
        {
            throw new NotImplementedException();
        }

        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand NavigateCommand { get; set; }

        public ViewDetailClientViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DeleteCommand = new DelegateCommand(ExecuteDelete);
            NavigateCommand = new DelegateCommand(ExecuteNavigate);
        }

        private void ExecuteDelete()
        {
            ClientDataManager.Delete(Item, ApplicationCommon.GetRequestProfile());
            _eventAggregator.GetEvent<ClientNavigationRequestedToList>().Publish(string.Empty);
        }

        private void ExecuteNavigate()
        {
            _eventAggregator.GetEvent<ClientNavigationRequestedToList>().Publish(string.Empty);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Count() > 0)
            {
                Item = new ClientDataModel();
                Item.ClientId = int.Parse(navigationContext.Parameters["ClientId"].ToString());

                var data = ClientDataManager.GetEntityDetails(Item, ApplicationCommon.GetRequestProfile());
                Item = data[0];
            }
            else
            {
                Item = new ClientDataModel();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

    }
}
