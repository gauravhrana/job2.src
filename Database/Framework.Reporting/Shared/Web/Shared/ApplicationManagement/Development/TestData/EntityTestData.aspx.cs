using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.Development.TestData
{
    public partial class EntityTestData : Shared.UI.WebFramework.BasePage
    {

        #region private methods

        private DataTable GetData()
        {
            var dt = Framework.Components.Audit.AuditHistory.GetEntityTestData();
            return dt;
        }

        private DataTable GetData(string entity, string typeofdata)
        {
            var dt = Framework.Components.Audit.AuditHistory.GetTestAndAuditDataIds(entity, typeofdata);
            return dt;
        }

        private string[] GetColumns()
        {
            var validColumns = new string[] { "SystemEntityType", "TestDataCount", "AuditHistoryCount" };
            return validColumns;
        }

        #endregion

        #region Events

        void oSearchFilter_OnSearch(object sender, EventArgs e)
        {
            TestAndAuditGrid.DataSource = GetData().DefaultView;
            TestAndAuditGrid.DataBind();
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //In-built paging implementation
            TestAndAuditGrid.DataSource = GetData();
            TestAndAuditGrid.PageIndex = e.NewPageIndex;
            TestAndAuditGrid.DataBind();
        }

        protected void GridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = GetData(e.CommandName, e.CommandArgument.ToString());
            StringCollection sc = new StringCollection();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                sc.Add(dt.Rows[i].ItemArray[1].ToString());
            }

            ShowSelectedRecords(sc, e.CommandName.ToString());
        }

        private void ShowSelectedRecords(StringCollection sc, string tablename)
        {
            string _tableFolder = Convert.ToString(ViewState["TableFolder"]);
            if (tablename.Equals("Schedule") || tablename.StartsWith("Schedule"))
                _tableFolder = "Scheduling";
            else if (tablename.Equals("Risk") || tablename.Equals("Reward"))
                _tableFolder = "RiskAndReward";
            else if (tablename.Equals("TaskRun") || tablename.StartsWith("TaskEntity") || tablename.StartsWith("TaskSchedule"))
                _tableFolder = "TasksAndWorkflow";
            else if (tablename.Equals("Activity") || tablename.StartsWith("TaskEntity") || tablename.StartsWith("TaskSchedule"))
                _tableFolder = "WBS";
            string _tableName = tablename;
            string redirecturl = "";

            string _tablePath = String.Empty;

            if (string.IsNullOrEmpty(_tableFolder.Trim()))
            {
                _tablePath = "~/" + _tableName;
            }
            else
            {
                _tablePath = "~/" + _tableFolder + "/" + _tableName;
            }

            if (sc.Count > 1)
            {
                redirecturl = _tablePath + "/Details.aspx?SuperKey=";


                int superKeyId = 0;
                var sData = new SystemEntityTypeDataModel();
                sData.EntityName = Convert.ToString(ViewState["TableName"]);
                var sDt = Framework.Components.Core.SystemEntityTypeDataManager.Search(sData, SessionVariables.AuditId);
                if (sDt.Rows.Count > 0)
                {
                    var systemEntityTypeId = Convert.ToInt32(sDt.Rows[0][SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId]);
                    superKeyId = ApplicationCommon.GenerateSuperKey(sc, systemEntityTypeId);
                }
                redirecturl += superKeyId;

            }
            else if(sc.Count == 1)
            {
                
                redirecturl = _tablePath + "/Details.aspx";
                redirecturl += "?SetId=" + sc[0];
            }
            Response.Redirect(redirecturl, false);


        }

        protected void GridView_RowDataBound(Object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                int nooftestrecords = int.Parse(e.Row.Cells[1].Text);
                int noofauditrecords = int.Parse(e.Row.Cells[2].Text);

                LinkButton lnk1 = (LinkButton)e.Row.Cells[3].FindControl("lnkView1");
                LinkButton lnk2 = (LinkButton)e.Row.Cells[4].FindControl("lnkView2");

                if (nooftestrecords <= 0)
                    lnk1.Enabled = false;
                if (noofauditrecords <= 0)
                    lnk2.Enabled = false;

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TestAndAuditGrid.DataSource = GetData().DefaultView;
                TestAndAuditGrid.DataBind();
            }
            oSearchFilter.OnSearch += oSearchFilter_OnSearch;
        }

        #endregion

    }
}