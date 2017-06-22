using DataModel.TaskTimeTracker;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System.Windows.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.Controls.ViewModels
{

    public class ClientListViewModel : ListViewModelBase
    {

        private ClientDataModel _searchItem = null;
        public ClientDataModel SearchItem
        {
            get { return _searchItem; }
            set { SetProperty(ref _searchItem, value); }
        }

        public ClientListViewModel(IRegionManager regionManager) : base(regionManager)
        {
            PrimaryEntity    = Framework.Components.DataAccess.SystemEntity.Client;
            PrimaryEntityKey = "Client";
            SettingCategory  = PrimaryEntityKey + "ListView";

            SearchItem = new ClientDataModel();
            SearchItem.Name = UserPerferenceUtility.GetUserPreferenceByKey("Name", SettingCategory);

            InitializeState();
        }

        protected override void SaveSearch()
        {
            UserPerferenceUtility.UpdateUserPreference(SettingCategory, "Name", SearchItem.Name);
        }

        protected override void RefreshData()
        {
            Data = new ListCollectionView(ClientDataManager.GetEntityDetails(SearchItem, ApplicationCommon.GetRequestProfile(), AuditDetailsFlag.DoNotFetchDetails.Value()));
        }

    }

}
