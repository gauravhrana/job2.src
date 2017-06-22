using System;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.Development.TestData
{
    public partial class RecordsWithMissingHistory : Shared.UI.WebFramework.BasePage
    {

        #region private methods

        private DataTable GetData()
        {
			//var dt = Framework.Components.Audit.AuditHistory.SearchRecordsWithMissingHistory(oSearchFilter.SystemEntityTypeId);
			//return dt;

			return null;
        }

        private string[] GetColumns()
        {
            var validColumns = new string[] { "SystemEntityType", "EntityKey", "NoHistoryRecords", "Reason" };
            return validColumns;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            oList.Setup("RecordsWithMissingHistory", " ", "Id", true, GetData, GetColumns, "TaskRun");
            myExportMenu.Setup("RecordsWithMissingHistory", String.Empty, GetData, GetColumns, String.Empty);

            if (Request.QueryString["Added"] != null)
                oList.SetSession(Request.QueryString["Added"]);
            else if (Request.QueryString["Deleted"] != null)
                oList.SetSession(Request.QueryString["Deleted"]);
            else
                oList.SetSession("true");

            oSearchFilter.OnSearch += oSearchFilter_OnSearch;

            Label lblStatus = ((Label)Master.FindControl("lblStatus"));
            var isTesting = SessionVariables.IsTesting;
            if (lblStatus != null && !(isTesting))
            {
                lblStatus.Visible = false;
            }
        }

        void oSearchFilter_OnSearch(object sender, EventArgs e)
        {
            oList.ShowData(false, true);
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("Insert.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        #endregion
    }
}