
using DataModel.TaskTimeTracker;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System;
using System.Linq;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.Controls.ViewModels
{
    public class EntitySaveViewModel : SaveViewModelBase
    {

        private ClientDataModel _item = null;
        public ClientDataModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public EntitySaveViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }

        protected override void ExecuteCancel()
        {
            RegionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(ClientViews.ListView, UriKind.Relative));
        }

        protected override void ExecuteSave()
        {
            if (Item.ClientId != null)
            {
                ClientDataManager.Update(Item, ApplicationCommon.GetRequestProfile());
            }
            else
            {
                ClientDataManager.Create(Item, ApplicationCommon.GetRequestProfile());
            }

            ExecuteCancel();
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
