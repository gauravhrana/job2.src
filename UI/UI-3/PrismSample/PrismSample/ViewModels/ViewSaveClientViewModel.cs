using DataModel.TaskTimeTracker;
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
using System.Windows.Navigation;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.ViewModels
{
    public class ViewSaveClientViewModel : BindableBase, INavigationAware
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

        public DelegateCommand SaveCommand { get; set; }

        public ViewSaveClientViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(ExecuteSave);
        }

        private bool CanExecuteSave()
        {
            return !String.IsNullOrWhiteSpace(Item.Name) && !String.IsNullOrWhiteSpace(Item.SortOrder.ToString());
        }

        private void ExecuteSave()
        {
            //LastUpdated = DateTime.Now;

            if (Item.ClientId != null)
            {
                ClientDataManager.Update(Item, ApplicationCommon.GetRequestProfile());
            }
            else
            {
                ClientDataManager.Create(Item, ApplicationCommon.GetRequestProfile());
            }
            _eventAggregator.GetEvent<ClientNavigationRequestedToList>().Publish(string.Empty);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if(navigationContext.Parameters.Count() > 0)
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
