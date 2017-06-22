using DataModel.TaskTimeTracker;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System;
using System.Windows.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.Controls.ViewModels
{

    public static class ClientViews
    {
        public const string ListView   = "EntityListView";
        public const string SaveView   = "EntitySaveView";
        public const string DetailView = "EntityDetailView";
    }

    public class EntityListViewModel : ListViewModelBase
    {

        private ClientDataModel _searchItem = null;
        public ClientDataModel SearchItem
        {
            get { return _searchItem; }
            set { SetProperty(ref _searchItem, value); }
        }

        public EntityListViewModel(IRegionManager regionManager) : base(regionManager)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Client;
            SettingCategory = "ClientListView";

            SearchItem = new ClientDataModel();
            SearchItem.Name = UserPerferenceUtility.GetUserPreferenceByKey("Name", SettingCategory);

            InitializeState();
        }

        protected override void RefreshData()
        {
            Data = new ListCollectionView(ClientDataManager.GetEntityDetails(SearchItem, ApplicationCommon.GetRequestProfile(), AuditDetailsFlag.DoNotFetchDetails.Value()));
        }

        protected override void ExecuteNavigateToDetail(int? id)
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", id);

            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(ClientViews.DetailView + parameters, UriKind.Relative));
        }

        protected override void ExecuteNavigateToSave()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(ClientViews.SaveView, UriKind.Relative));
        }

        protected override void ExecuteNavigateToEdit(int? id)
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", id);

            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(ClientViews.SaveView + parameters, UriKind.Relative));
        }

    }

}
