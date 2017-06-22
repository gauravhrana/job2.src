using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.FeatureRuleStatus
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new FeatureRuleStatusDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FeatureRuleStatusId =
					Convert.ToInt32(SelectedData.Rows[i][FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId].ToString());
				data.Name =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FeatureRuleStatusDataModel();
				data.FeatureRuleStatusId = Convert.ToInt32(SelectedData.Rows[i][FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}
		
		protected override DataTable GetEntityData(int? entityKey)
		{
			var featureRuleStatusdata = new FeatureRuleStatusDataModel();
			featureRuleStatusdata.FeatureRuleStatusId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.Search(featureRuleStatusdata, SessionVariables.RequestProfile);
			return results;
		}	

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleStatus;
			PrimaryEntityKey = "FeatureRuleStatus";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}