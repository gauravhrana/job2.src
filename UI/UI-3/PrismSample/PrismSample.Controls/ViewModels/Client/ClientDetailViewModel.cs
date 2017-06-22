using DataModel.TaskTimeTracker;
using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System.Linq;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.Controls.ViewModels
{

    public class ClientDetailViewModel : DetailViewModelBase
    {

        private ClientDataModel _item = null;
        public ClientDataModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public ClientDetailViewModel(IRegionManager regionManager) : base(regionManager)
        {
            PrimaryEntity    = Framework.Components.DataAccess.SystemEntity.Client;
            PrimaryEntityKey = "Client";
        }

        protected override void ExecuteDelete()
        {
            ClientDataManager.Delete(Item, ApplicationCommon.GetRequestProfile());

            NavigateToListView();
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
