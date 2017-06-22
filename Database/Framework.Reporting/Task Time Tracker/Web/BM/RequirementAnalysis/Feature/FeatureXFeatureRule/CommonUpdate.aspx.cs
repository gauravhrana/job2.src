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

namespace ApplicationContainer.UI.Web.FeatureXFeatureRule
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new FeatureXFeatureRuleDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FeatureXFeatureRuleId =
					Convert.ToInt32(SelectedData.Rows[i][FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId].ToString());
				data.FeatureId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FeatureXFeatureRuleDataModel.DataColumns.FeatureId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FeatureXFeatureRuleDataModel.DataColumns.FeatureId))
					: int.Parse(SelectedData.Rows[i][FeatureXFeatureRuleDataModel.DataColumns.FeatureId].ToString());
				data.FeatureRuleId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId))
					: int.Parse(SelectedData.Rows[i][FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId].ToString());

				data.FeatureRuleStatusId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId).ToString())
					: int.Parse(SelectedData.Rows[i][FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId].ToString());

                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FeatureXFeatureRuleDataModel();
				data.FeatureXFeatureRuleId = Convert.ToInt32(SelectedData.Rows[i][FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}
		
		protected override DataTable GetEntityData(int? entityKey)
		{
			var featureXFeatureRuledata = new FeatureXFeatureRuleDataModel();
			featureXFeatureRuledata.FeatureXFeatureRuleId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Search(featureXFeatureRuledata, SessionVariables.RequestProfile);
			return results;
		}	

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule;
			PrimaryEntityKey = "FeatureXFeatureRule";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
    
    }
}