using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using DataModel.Framework.Configuration;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Prototype.Prototype.TestData
{
	public partial class EntityTestData : Framework.UI.Web.BaseClasses.PageBasePage
    {

		private int DetailUserPreferenceCategoryId
		{
			get
			{
				return Convert.ToInt32(ViewState["DetailUserPreferenceCategoryId"]);
			}
			set
			{
				ViewState["DetailUserPreferenceCategoryId"] = value;
			}
		}

        #region private methods		

		private void DataBind(int appId,string appName, string entityName)
		{
			var dt=new DataTable();

			dt = Framework.Components.Core.SystemEntityTypeDataManager.EntityTestData(SessionVariables.RequestProfile, appId, appName.Replace(" ", string.Empty), entityName);
			
			TestAndAuditGrid.DataSource = dt;
			
			TestAndAuditGrid.DataBind();
		}
        #endregion

        #region Events		

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
				var sDt = Framework.Components.Core.SystemEntityTypeDataManager.GetDetails(sData, SessionVariables.RequestProfile);
                if (sDt != null)
                {
                    var systemEntityTypeId = sDt.SystemEntityTypeId.Value;
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

		protected override void OnInit(EventArgs e)
		{
			//SettingCategory = "AuditHistoryDefaultView";
			DetailUserPreferenceCategoryId = PreferenceUtility.CreateUserPreferenceCategoryIfNotExists("AuditHistory", "AuditHistory");

			//oSearchFilter.SearchControl.SettingCategory = SettingCategory + "SearchControl";

			//oSearchFilter.GetFilter(SystemEntity.AuditHistory, "AuditHistoryId");

			SettingCategory = "EntityTestDataDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;

			//TestAndAuditGrid.CssClass = "table table-hover table-striped";

		}		

		protected void Page_Load(object sender, EventArgs e)
		{		

			if (!IsPostBack)
			{
				var appData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);				
				UIHelper.LoadDropDown(appData, ddlApplication, DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.Name,
					DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.ApplicationId);

				ddlApplication.Items.Insert(0, new ListItem("All", "-1"));

				var data = new SystemEntityTypeDataModel();
				
				if(Convert.ToInt32(ddlApplication.SelectedValue.ToString()) !=-1)
					data.ApplicationId = Convert.ToInt32(ddlApplication.SelectedValue.ToString());
				else
					data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

				var dt = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
				
				UIHelper.LoadDropDown(dt,
					drpSystemEntity, SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
				drpSystemEntity.Items.Insert(0, new ListItem("All", "-1"));

				var dbData = Framework.Components.DataAccess.SetupConfiguration.GetListConnectionKeyName(SessionVariables.RequestProfile.AuditId);
				UIHelper.LoadDropDown(dbData, drpDbName, "ConnectionKeyName","ConnectionKeyName");
				
			}
			
		}
		
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var appName = drpDbName.SelectedItem.ToString();
			var appId = ddlApplication.SelectedValue.ToString();
			var strEntity = drpSystemEntity.SelectedItem.ToString();

			//if(appId!=-1)
			DataBind(Convert.ToInt32(appId), appName,strEntity);
			
		}

		protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
		{			
			drpSystemEntity.Items.Clear();
			TestAndAuditGrid.DataSource = null;
			TestAndAuditGrid.DataBind();
			string strApplication = string.Empty;
			strApplication = ddlApplication.SelectedValue;

			//if (ddlApplication.SelectedIndex != 0)
			//{
				var data = new SystemEntityTypeDataModel();

				if (Convert.ToInt32(ddlApplication.SelectedValue.ToString()) != -1)
					data.ApplicationId = Convert.ToInt32(ddlApplication.SelectedValue.ToString());
				else
					data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

				var dt = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

				UIHelper.LoadDropDown(dt,
					drpSystemEntity, SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
				drpSystemEntity.Items.Insert(0, new ListItem("All", "-1"));
			//}
		}
        #endregion

    }
}