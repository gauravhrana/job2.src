using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{

		
        protected override DataTable GetEntityData(int? entityKey)
        {
            var fesdata = new FunctionalityEntityStatusDataModel();
            fesdata.FunctionalityEntityStatusId = entityKey;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.GetEntityDetails(fesdata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }		

        protected override DataTable UpdateData()
        {
			var UpdatedData = new List<FunctionalityEntityStatusDataModel>();
			var data = new FunctionalityEntityStatusDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FunctionalityEntityStatusId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId].ToString());
				data.FunctionalityId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityId].ToString());
				data.FunctionalityPriorityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId).ToString())
					: int.Parse(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityPriorityId].ToString());

				data.FunctionalityStatusId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId).ToString())
					: int.Parse(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityStatusId].ToString());

				data.AssignedTo = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.AssignedTo))
					? CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.AssignedTo)
					: SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.AssignedTo].ToString();
				data.Memo =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.Memo))
					? CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.Memo)
					: SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.Memo].ToString();
				data.SystemEntityTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.SystemEntityTypeId].ToString());

				data.TargetDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.TargetDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.TargetDate).ToString())
                    : DateTime.Parse(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.TargetDate].ToString());

				data.StartDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.StartDate))
                    ? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityEntityStatusDataModel.DataColumns.StartDate).ToString())
                    : DateTime.Parse(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.StartDate].ToString());

				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FunctionalityEntityStatusDataModel();
				data.FunctionalityEntityStatusId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityEntityStatusDataModel.DataColumns.FunctionalityEntityStatusId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
				// if everything is done and good THEN move from thsi page.
				//Response.Redirect("Default.aspx?Added=" + true, false);

			}
            return UpdatedData.ToDataTable();
			//DynamicUpdatePanel.SetUp(GetColumns(), "FunctionalityEntityStatus", UpdatedData);
		}

		protected override void OnInit(EventArgs e)
		{
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus;
            PrimaryEntityKey = "FunctionalityEntityStatus";
            BreadCrumbObject = Master.BreadCrumbObject;
		}
	}
}