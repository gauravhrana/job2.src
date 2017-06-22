using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Data;
using System;
using Framework.Components.DataAccess;
using Microsoft.Practices.Prism.Regions;

namespace PrismSample.Infrastructure
{
    public abstract class ListViewModelBase : ViewModelBase
    {

        protected string DetailsControlPath { get; set; }

        private ObservableCollection<DataGridColumn> _columnCollection;
        public ObservableCollection<DataGridColumn> ColumnCollection
        {
            get { return _columnCollection; }
            set
            {
                SetProperty(ref _columnCollection, value);
            }
        }

        private ICollectionView _fcModes = null;
        public ICollectionView FCModes
        {
            get { return _fcModes; }
            set
            {
                SetProperty(ref _fcModes, value);
            }
        }

        private string _selectedFCMode;
        public string SelectedFCMode
        {
            get
            {
                return _selectedFCMode;
            }
            set
            {
                if (_selectedFCMode != value)
                {
                    SetProperty<string>(ref _selectedFCMode, value);

                    // Perform any post-notification process here.
                    IsBusy = true;
                    RefreshColumns();
                    IsBusy = false;

                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty<bool>(ref _isBusy, value); //("IsBusy");
            }
        }

        private ICollectionView _data = null;
        public ICollectionView Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        public DelegateCommand<int?> NavigateToEditCommand { get; set; }
        public DelegateCommand<int?> NavigateToDetailCommand { get; set; }

        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }

        public ListViewModelBase(IRegionManager regionManager) : base(regionManager)
        { }

        protected virtual void InitializeState()
        {

            NavigateToEditCommand = new DelegateCommand<int?>(ExecuteNavigateToEdit);
            NavigateToDetailCommand = new DelegateCommand<int?>(ExecuteNavigateToDetail);
            SearchCommand = new DelegateCommand(ExecuteSearch);
            AddCommand = new DelegateCommand(ExecuteNavigateToSave);

            IsBusy = true;

            RefreshModes();
            RefreshData();

            IsBusy = false;
        }

        protected virtual void RefreshData()
        {
            throw new NotImplementedException();
        }

        protected virtual void SaveSearch()
        {
            throw new NotImplementedException();
        }

        protected void ExecuteNavigateToSave()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(PrimaryEntityKey + "SaveView", UriKind.Relative));
        }

        protected void ExecuteNavigateToDetail(int? id)
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", id);

            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(PrimaryEntityKey + "DetailView" + parameters, UriKind.Relative));
        }

        protected void ExecuteNavigateToEdit(int? id)
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", id);

            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(PrimaryEntityKey + "SaveView" + parameters, UriKind.Relative));
        }

        private void RefreshColumns()
        {
            var fcRecords = FieldConfigurationUtility.GetFieldConfigurations(PrimaryEntity, int.Parse(SelectedFCMode));

            var tmpColumns = new List<DataGridColumn>();

            foreach (var fcRecord in fcRecords)
            {
                tmpColumns.Add(new DataGridTextColumn
                {
                    Header = fcRecord.FieldConfigurationDisplayName,
                    Binding = new Binding(fcRecord.Name),
                    Width = new DataGridLength(Double.Parse(fcRecord.Width.ToString()))
                });
            }

            ColumnCollection = null;
            ColumnCollection = new ObservableCollection<DataGridColumn>(tmpColumns);
        }

        private void RefreshModes()
        {
            var fcModes = FieldConfigurationUtility.GetApplicableModesList(PrimaryEntity);
            FCModes = new ListCollectionView(fcModes);
            SelectedFCMode = fcModes[0].FieldConfigurationModeId.ToString();
        }

        protected virtual void ExecuteSearch()
        {
            IsBusy = true;
            SaveSearch();
            RefreshData();
            IsBusy = false;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ColumnCollection = null; 
        }

    }
}
