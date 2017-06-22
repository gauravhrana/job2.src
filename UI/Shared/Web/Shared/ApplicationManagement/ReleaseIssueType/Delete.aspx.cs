using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
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
			PrimaryEntityKey = "ReleaseIssueType";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ReleaseIssueType", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
			ShowAuditHistory(chkVisible.Checked);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string[] deleteIndexList = DeleteIds.Split(',');
            foreach (string index in deleteIndexList)
            {
                var data = new ReleaseIssueTypeDataModel();
                data.ReleaseIssueTypeId = int.Parse(index);
				Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.Delete(data, SessionVariables.AuditId);
            }

			DeleteAndRedirect();
        }

		private void DeleteAndRedirect()
		{
            Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ReleaseIssueType, AuditId);
			Response.Redirect(Page.GetRouteUrl("ReleaseIssueTypeEntityRoute", new { Action = "Default", SetId = true }), false);
		}
        

        #endregion
    }
}