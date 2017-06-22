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

namespace ApplicationContainer.UI.Web.Feature.FeatureRule
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new FeatureRuleDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FeatureRuleId =
					Convert.ToInt32(SelectedData.Rows[i][FeatureRuleDataModel.DataColumns.FeatureRuleId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.FeatureRuleCategoryId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId).ToString())
					: int.Parse(SelectedData.Rows[i][FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId].ToString());

                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FeatureRuleDataModel();
				data.FeatureRuleId = Convert.ToInt32(SelectedData.Rows[i][FeatureRuleDataModel.DataColumns.FeatureRuleId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var featureRuledata = new FeatureRuleDataModel();
			featureRuledata.FeatureRuleId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Search(featureRuledata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRule;
			PrimaryEntityKey = "FeatureRule";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}