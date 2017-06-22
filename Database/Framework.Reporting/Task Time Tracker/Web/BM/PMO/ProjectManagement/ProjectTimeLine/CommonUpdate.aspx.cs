using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectTimeLine
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ProjectTimeLineDataModel();
            UpdatedData = ProjectTimeLineDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ProjectTimeLineId =
					Convert.ToInt32(SelectedData.Rows[i][ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId].ToString());
				data.ProjectId = Convert.ToInt32(SelectedData.Rows[i][ProjectTimeLineDataModel.DataColumns.ProjectId].ToString());

                data.StartDate =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectTimeLineDataModel.DataColumns.StartDate))
                    ? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ProjectTimeLineDataModel.DataColumns.StartDate).ToString())
                    : DateTime.Parse(SelectedData.Rows[i][ProjectTimeLineDataModel.DataColumns.StartDate].ToString());

                data.EndDate =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectTimeLineDataModel.DataColumns.EndDate))
                    ? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ProjectTimeLineDataModel.DataColumns.EndDate).ToString())
                    : DateTime.Parse(SelectedData.Rows[i][ProjectTimeLineDataModel.DataColumns.EndDate].ToString());

                ProjectTimeLineDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ProjectTimeLineDataModel();
				data.ProjectTimeLineId = Convert.ToInt32(SelectedData.Rows[i][ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId].ToString());
                var dt = ProjectTimeLineDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}			
			}

			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var projectTimeLinedata = new ProjectTimeLineDataModel();
			projectTimeLinedata.ProjectTimeLineId = entityKey;
            var results = ProjectTimeLineDataManager.Search(projectTimeLinedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectTimeLine;
			PrimaryEntityKey = "ProjectTimeLine";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}