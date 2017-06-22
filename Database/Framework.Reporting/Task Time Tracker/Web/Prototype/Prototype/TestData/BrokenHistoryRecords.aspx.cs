using System;
using System.Data;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.Development.TestData
{
	public partial class BrokenHistoryRecords : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region properties

        private bool isSearch
        {
            get
            {
                if (ViewState["IsSearch"] == null)
                    return false;
                else
                    return Convert.ToBoolean(ViewState["IsSearch"]);
            }
            set
            {
                ViewState["IsSearch"] = value;
            }
        }

        #endregion

        #region private methods

        private DataTable GetData()
        {
            DataModel.Framework.Audit.AuditHistory data = oSearchFilter.SearchParameters;

            if (!isSearch && !IsPostBack)
            {
                data.SystemEntityId = -1;
                //data.TypeOfIssue = "-1";
            }

			var dt = Framework.Components.Audit.AuditHistoryDataManager.SearchBrokenHistoryRecords(data, SessionVariables.RequestProfile);

            return dt;

            //return null;
        }

        private string[] GetColumns()
        {
			var validColumns = FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.AuditHistory, "Find", SessionVariables.RequestProfile);
            return validColumns;
        }

        void oSearchFilter_OnSearch(object sender, EventArgs e)
        {
            isSearch = true;
            oList.ShowData(false, true);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            oList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, GetData, GetColumns, "AuditHistory");
            //oList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, GetData, GetColumns, true, false, false, "AuditHistory");
            //myExportMenu.Setup("AuditHistory", String.Empty, GetData, GetColumns, oSearchFilter.SearchParameters.ToURLQuery());

            if (Request.QueryString["Added"] != null)
                oList.SetSession(Request.QueryString["Added"]);
            else if (Request.QueryString["Deleted"] != null)
                oList.SetSession(Request.QueryString["Deleted"]);
            else
                oList.SetSession("true");

            oSearchFilter.OnSearch += oSearchFilter_OnSearch;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("Insert.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:1206/Default.aspx");
        }

        #endregion

    }
}