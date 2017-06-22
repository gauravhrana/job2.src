using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using DataModel.Framework.ReleaseLog;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

		#region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.AddTab("Release Issue Type", detailsControl, String.Empty, true);

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Release Log Detail", listControl);

			listControl.Setup("ReleaseLogDetail", String.Empty, "ReleaseLogDetailId", setId, true, GetData, GetReleaseLogDetailColumns, "ReleaseLogDetail");
			listControl.SetSession("true");

			tabControl.Setup("ReleaseIssueTypeView");

			return tabControl;
		}

		private DataTable GetData(string key)
		{
			return GetReleaseLogDetailData(int.Parse(key));
		}

		private DataTable GetReleaseLogDetailData(int releaseIssueTypeId)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetByReleaseLogIssueType(releaseIssueTypeId, AuditId);
			var releaseLogDetaildt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetList(AuditId);
			var resultdt = releaseLogDetaildt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = releaseLogDetaildt.Select("ReleaseLogDetailId = " + row[ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetReleaseLogDetailColumns()
		{
			return Framework.Components.ApplicationSecurity.GetReleaseLogDetailColumns("DBColumns", AuditId);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseIssueType;

			GenericControlPath = ApplicationCommon.GetControlPath("ReleaseIssueType", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "ReleaseIssueType";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;
		}

		#endregion        
        
    }
}