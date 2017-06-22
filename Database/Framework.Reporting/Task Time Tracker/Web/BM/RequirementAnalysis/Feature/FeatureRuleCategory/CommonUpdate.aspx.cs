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

namespace ApplicationContainer.UI.Web.Feature.FeatureRuleCategory
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new FeatureRuleCategoryDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FeatureRuleCategoryId =
					Convert.ToInt32(SelectedData.Rows[i][FeatureRuleCategoryDataModel.DataColumns.FeatureRuleCategoryId].ToString());
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

                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FeatureRuleCategoryDataModel();
				data.FeatureRuleCategoryId = Convert.ToInt32(SelectedData.Rows[i][FeatureRuleCategoryDataModel.DataColumns.FeatureRuleCategoryId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var featureRuleCategorydata = new FeatureRuleCategoryDataModel();
			featureRuleCategorydata.FeatureRuleCategoryId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.Search(featureRuleCategorydata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleCategory;
			PrimaryEntityKey = "FeatureRuleCategory";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
		
	}
}