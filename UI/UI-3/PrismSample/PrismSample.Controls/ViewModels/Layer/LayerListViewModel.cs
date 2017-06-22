using Microsoft.Practices.Prism.Regions;
using PrismSample.Infrastructure;
using System.Windows.Data;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;

namespace PrismSample.Controls.ViewModels
{

	public class LayerListViewModel : ListViewModelBase
	{

		private LayerDataModel _searchItem = null;
		public LayerDataModel SearchItem
		{
			get { return _searchItem; }
			set { SetProperty(ref _searchItem, value); }
		}

		public LayerListViewModel(IRegionManager regionManager) : base(regionManager)
		{
			PrimaryEntity    = Framework.Components.DataAccess.SystemEntity.Layer;
			PrimaryEntityKey = "Layer";
			SettingCategory  = PrimaryEntityKey + "ListView";

			SearchItem = new LayerDataModel();
			SearchItem.Name = UserPerferenceUtility.GetUserPreferenceByKey("Name", SettingCategory);

			InitializeState();
		}

		protected override void SaveSearch()
		{
			UserPerferenceUtility.UpdateUserPreference(SettingCategory, "Name", SearchItem.Name);
		}

		protected override void RefreshData()
		{
			Data = new ListCollectionView(LayerDataManager.GetEntityDetails(SearchItem, ApplicationCommon.GetRequestProfile(), AuditDetailsFlag.DoNotFetchDetails.Value()));
		}

	}

}
