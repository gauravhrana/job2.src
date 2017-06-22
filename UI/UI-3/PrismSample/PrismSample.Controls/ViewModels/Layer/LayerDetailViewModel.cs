using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System.Linq;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.Controls.ViewModels
{
	public class LayerDetailViewModel : DetailViewModelBase
	{

		private LayerDataModel _item = null;
		public LayerDataModel Item
		{
			get { return _item; }
			set { SetProperty(ref _item, value); }
		}

		public LayerDetailViewModel(IRegionManager regionManager) : base(regionManager)
		{
			PrimaryEntity    = Framework.Components.DataAccess.SystemEntity.Layer;
			PrimaryEntityKey = "Layer";
		}

		protected override void ExecuteDelete()
		{
			LayerDataManager.Delete(Item, ApplicationCommon.GetRequestProfile());
			NavigateToListView();
		}

		public override void OnNavigatedTo(NavigationContext navigationContext)
		{
			if (navigationContext.Parameters.Count() > 0)
			{
				Item = new LayerDataModel();
				Item.LayerId = int.Parse(navigationContext.Parameters["id"].ToString());

				var data = LayerDataManager.GetEntityDetails(Item, ApplicationCommon.GetRequestProfile());
				Item = data[0];
			}
			else
			{
				Item = new LayerDataModel();
			}
		}

	}
}
