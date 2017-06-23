using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogStatus
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
	{
		#region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.AddTab("Release Log Status", detailsControl, String.Empty, true);

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Release Log", listControl);

			listControl.Setup("ReleaseLog", String.Empty, "ReleaseLogId", setId, true, GetData, GetReleaseLogColumns, "ReleaseLog");
			listControl.SetSession("true");

			tabControl.Setup("ReleaseLogStatusView");

			return tabControl;
		}

		private DataTable GetData(string key)
		{
			return GetReleaseLogData(int.Parse(key));
		}

		private DataTable GetReleaseLogData(int releaseLogStatusId)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetByReleaseLogStatus(releaseLogStatusId, SessionVariables.RequestProfile);
			var releaseLogdt = Framework.Components.ReleaseLog.ReleaseLogDataManager.Search(
                new ReleaseLogDataModel(){ ReleaseLogId = -1}, SessionVariables.RequestProfile);
			var resultdt = releaseLogdt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = releaseLogdt.Select("ReleaseLogId = " + row[ReleaseLogDataModel.DataColumns.ReleaseLogId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetReleaseLogColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLog, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetReleaseLogDetailData(int releaseLogStatusId)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetByReleaseLogStatus(releaseLogStatusId, SessionVariables.RequestProfile);
			var releaseLogdt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Search(
                new ReleaseLogDetailDataModel(){ReleaseLogDetailId = -1}, SessionVariables.RequestProfile);
			var resultdt = releaseLogdt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = releaseLogdt.Select("ReleaseLogDetailId = " + row[ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetReleaseLogDetailColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail, "DBColumns", SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogStatus;

			GenericControlPath = ApplicationCommon.GetControlPath("ReleaseLogStatus", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "ReleaseLogStatus";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;
		}

		#endregion
		
	}
}