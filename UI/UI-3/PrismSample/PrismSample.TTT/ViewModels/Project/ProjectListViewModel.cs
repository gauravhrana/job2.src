using DataModel.TaskTimeTracker.RequirementAnalysis;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System;
using System.Windows.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.TTT.ViewModels
{

    public class ProjectListViewModel : ListViewModelBase
    {

        private ProjectDataModel _searchItem = null;
        public ProjectDataModel SearchItem
        {
            get { return _searchItem; }
            set { SetProperty(ref _searchItem, value); }
        }

        public ProjectListViewModel(IRegionManager regionManager) : base(regionManager)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Project;
            PrimaryEntityKey = "Project";
            SettingCategory = PrimaryEntityKey + "ListView";

            SearchItem = new ProjectDataModel();
            SearchItem.Name = UserPerferenceUtility.GetUserPreferenceByKey("Name", SettingCategory);

            InitializeState();
        }

        protected override void SaveSearch()
        {
            UserPerferenceUtility.UpdateUserPreference(SettingCategory, "Name", SearchItem.Name);
        }

        protected override void RefreshData()
        {
            Data = new ListCollectionView(ProjectDataManager.GetEntityDetails(SearchItem, ApplicationCommon.GetRequestProfile(), AuditDetailsFlag.DoNotFetchDetails.Value()));
        }

    }
}
