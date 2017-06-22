using System;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.TaskTimeTracker;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismSample.Events;
using System.Windows.Data;
using TaskTimeTracker.Components.BusinessLayer;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PrismSample.ViewModels
{
    public class ViewListClientViewModel : BindableBase, INavigationAware
    {

        private readonly IEventAggregator _eventAggregator;

        private ClientDataModel _searchItem = null;
        public ClientDataModel SearchItem
        {
            get { return _searchItem; }
            set { SetProperty(ref _searchItem, value); }
        }

        private ObservableCollection<DataGridColumn> _fCColumns;
        public ObservableCollection<DataGridColumn> FCColumns
        {
            get { return _fCColumns; }
            set { SetProperty(ref _fCColumns, value); }
        }

        private ICollectionView _clients = null;
        public ICollectionView Clients
        {
            get { return _clients; }
            set { SetProperty(ref _clients, value); }
        }

        public DelegateCommand<int?> NavigateToEditCommand { get; set; }
        public DelegateCommand<int?> NavigateToDetailCommand { get; set; }

        public DelegateCommand SearchCommand { get; set; }

        private void RefreshData()
        {
            this.Clients = new ListCollectionView(ClientDataManager.GetEntityDetails(SearchItem, ApplicationCommon.GetRequestProfile()));
        }

        private void RefreshColumns()
        {
            var fcRecords = ApplicationCommon.GetFieldConfigurations("Client");

            FCColumns = new ObservableCollection<DataGridColumn>();

            foreach(var fcRecord in fcRecords)
            {
                FCColumns.Add(new DataGridTextColumn
                {
                    Header = fcRecord.FieldConfigurationDisplayName,
                    Binding = new Binding(fcRecord.Name),
                    Width = new DataGridLength(Double.Parse(fcRecord.Width.ToString()))
                });
            }
        }

        public ViewListClientViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            NavigateToEditCommand = new DelegateCommand<int?>(NavigateToEdit);
            NavigateToDetailCommand = new DelegateCommand<int?>(NavigateToDetail);
            SearchCommand = new DelegateCommand(ExecuteSearch);

            SearchItem = new ClientDataModel();

            RefreshColumns();
            RefreshData();

            //this.Clients = new ListCollectionView(clients);
            this.Clients.CurrentChanged += SelectedItemChanged;
        }

        private void ExecuteSearch()
        {
            RefreshData();
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void NavigateToEdit(int? clientId)
        {
            _eventAggregator.GetEvent<ClientNavigationRequestedToEdit>().Publish(clientId.ToString());
        }

        private void NavigateToDetail(int? clientId)
        {
            _eventAggregator.GetEvent<ClientNavigationRequestedToDetail>().Publish(clientId.ToString());
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            RefreshData();
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
