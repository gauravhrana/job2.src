using DataModel.TaskTimeTracker;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System;
using System.Linq;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.Controls.ViewModels
{

    public class EntityDetailViewModel : DetailViewModelBase
    {

        private ClientDataModel _item = null;
        public ClientDataModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public EntityDetailViewModel(IRegionManager regionManager) : base(regionManager)
        { }

        protected override void ExecuteDelete()
        {
            ClientDataManager.Delete(Item, ApplicationCommon.GetRequestProfile());

            ExecuteNavigatation();
        }

        protected override void ExecuteNavigatation()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(ClientViews.ListView, UriKind.Relative));
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Count() > 0)
            {
                Item = new ClientDataModel();
                Item.ClientId = int.Parse(navigationContext.Parameters["id"].ToString());

                var data = ClientDataManager.GetEntityDetails(Item, ApplicationCommon.GetRequestProfile());
                Item = data[0];
            }
            else
            {
                Item = new ClientDataModel();
            }
        }

    }
}
