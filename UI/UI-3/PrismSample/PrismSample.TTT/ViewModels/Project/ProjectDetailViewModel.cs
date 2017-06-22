using DataModel.TaskTimeTracker.RequirementAnalysis;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System;
using System.Linq;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.TTT.ViewModels
{
    public class ProjectDetailViewModel : DetailViewModelBase
    {

        private ProjectDataModel _item = null;
        public ProjectDataModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public ProjectDetailViewModel(IRegionManager regionManager) : base(regionManager)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Project;
            PrimaryEntityKey = "Project";
        }

        protected override void ExecuteDelete()
        {
            ProjectDataManager.Delete(Item, ApplicationCommon.GetRequestProfile());

            NavigateToListView();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Count() > 0)
            {
                Item = new ProjectDataModel();
                Item.ProjectId = int.Parse(navigationContext.Parameters["id"].ToString());

                var data = ProjectDataManager.GetEntityDetails(Item, ApplicationCommon.GetRequestProfile());
                Item = data[0];
            }
            else
            {
                Item = new ProjectDataModel();
            }
        }

    }
}
