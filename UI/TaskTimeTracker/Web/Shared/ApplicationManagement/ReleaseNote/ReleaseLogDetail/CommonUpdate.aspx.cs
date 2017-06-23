using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using Dapper;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogDetail
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<ReleaseLogDetailDataModel>();
			var data = new ReleaseLogDetailDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ReleaseLogDetailId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId].ToString());
				data.ReleaseLogId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ReleaseLogId].ToString());
				data.ReleaseIssueTypeId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ReleaseIssueTypeId].ToString());
				data.ReleasePublishCategoryId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ReleasePublishCategoryId].ToString());
				data.ModuleId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ModuleId].ToString());
				data.ReleaseFeatureId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ReleaseFeatureId].ToString());
				data.ItemNo =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ItemNo].ToString());
				data.RequestedBy =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.RequestedBy))
					? CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.RequestedBy)
					: SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.RequestedBy].ToString();
				data.PrimaryDeveloper =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper))
					? CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper)
					: SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.PrimaryDeveloper].ToString();
				data.JIRA =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.JIRA))
					? CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.JIRA)
					: SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.JIRA].ToString();
				data.Feature =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.Feature))
					? CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.Feature)
					: SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.Feature].ToString();				
				data.PrimaryEntity = SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.PrimaryEntity].ToString();
				data.TimeSpent =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.TimeSpent))
					? CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.TimeSpent)
					: SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.TimeSpent].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.Description)
					: SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ReleaseLogDetailDataModel.DataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.SortOrder].ToString());
				
				data.RequestedDate = Convert.ToDateTime(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.RequestedDate].ToString());

				Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Save(data, SessionVariables.RequestProfile, "Update");
				data = new ReleaseLogDetailDataModel();
				data.ReleaseLogDetailId = Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId].ToString());
				data.ReleaseLogId = Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDetailDataModel.DataColumns.ReleaseLogId].ToString());
				var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var releaseLogDetailData = new ReleaseLogDetailDataModel();
			releaseLogDetailData.ReleaseLogDetailId = entityKey;
			var results = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetEntityDetails(releaseLogDetailData, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail;
			PrimaryEntityKey = "ReleaseLogDetail";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}