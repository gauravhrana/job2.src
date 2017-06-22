using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.MilestoneXFeature
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods	 	

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new MilestoneXFeatureDataModel();
            UpdatedData = MilestoneXFeatureDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.MilestoneXFeatureId =
					Convert.ToInt32(SelectedData.Rows[i][MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId].ToString());
                
				data.MilestoneFeatureStateId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId))
                    : int.Parse(SelectedData.Rows[i][MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId].ToString());
				data.MilestoneId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MilestoneXFeatureDataModel.DataColumns.MilestoneId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(MilestoneXFeatureDataModel.DataColumns.MilestoneId))
                    : int.Parse(SelectedData.Rows[i][MilestoneXFeatureDataModel.DataColumns.MilestoneId].ToString());

                data.FeatureId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(MilestoneXFeatureDataModel.DataColumns.FeatureId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(MilestoneXFeatureDataModel.DataColumns.FeatureId).ToString())
                    : int.Parse(SelectedData.Rows[i][MilestoneXFeatureDataModel.DataColumns.FeatureId].ToString());

				data.Memo = SelectedData.Rows[i][MilestoneXFeatureDataModel.DataColumns.Memo].ToString();

                MilestoneXFeatureDataManager.Update(data, SessionVariables.RequestProfile);
				data = new MilestoneXFeatureDataModel();
				data.MilestoneXFeatureId =	Convert.ToInt32(SelectedData.Rows[i][MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId].ToString());
                var dt = MilestoneXFeatureDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}					
			}
			return UpdatedData;
		}       

		protected override DataTable GetEntityData(int? entityKey)
		{
			var milestoneXFeaturedata = new MilestoneXFeatureDataModel();
			milestoneXFeaturedata.MilestoneXFeatureId = entityKey;
            var results = MilestoneXFeatureDataManager.Search(milestoneXFeaturedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.MilestoneXFeature;
			PrimaryEntityKey = "MilestoneXFeature";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	
    }
}