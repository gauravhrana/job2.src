using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using System.Data;
using System.Web;
using System.Web.UI;
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
	public partial class RecordsWithMissingHistory : Framework.UI.Web.BaseClasses.PageBasePage
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

		private void GetData(int systemEntityTypeId)
        {
			var dt = Framework.Components.Audit.AuditHistoryDataManager.SearchRecordsWithMissingHistory(systemEntityTypeId, SessionVariables.RequestProfile);
			TestAndAuditGrid.DataSource = dt;
			TestAndAuditGrid.DataBind();
			//return dt;

			//return null;
        }

		//private string[] GetColumns()
		//{
		//	var validColumns = new string[] { "SystemEntityType", "EntityKey", "NoHistoryRecords", "Reason" };
		//	return validColumns;
		//}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			//SettingCategory = "AuditHistoryDefaultView";
			DetailUserPreferenceCategoryId = PreferenceUtility.CreateUserPreferenceCategoryIfNotExists("AuditHistory", "AuditHistory");

			SettingCategory = "RecordsWithMissingHistoryDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;

		}	

        protected void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				var data = new SystemEntityTypeDataModel();
				
				data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

				var dt = Framework.Components.Core.SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

				UIHelper.LoadDropDown(dt,
					drpSystemEntity, SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
				//drpSystemEntity.Items.Insert(0, new ListItem("All", "-1"));
			}

			//oList.Setup("AuditHistory", " ", "AuditHistoryId", true, GetData, GetColumns, string.Empty);
        //    myExportMenu.Setup("RecordsWithMissingHistory", String.Empty, GetData, GetColumns, String.Empty);

			//if (Request.QueryString["Added"] != null)
			//	oList.SetSession(Request.QueryString["Added"]);
			//else if (Request.QueryString["Deleted"] != null)
			//	oList.SetSession(Request.QueryString["Deleted"]);
			//else
			//	oList.SetSession("true");

			//oSearchFilter.OnSearch += oSearchFilter_OnSearch;

            Label lblStatus = ((Label)Master.FindControl("lblStatus"));
            var isTesting = SessionVariables.IsTesting;
            if (lblStatus != null && !(isTesting))
            {
                lblStatus.Visible = false;
            }
        }

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var strEntity = drpSystemEntity.SelectedValue;
			GetData(Convert.ToInt32(strEntity));

		}

		//void oSearchFilter_OnSearch(object sender, EventArgs e)
		//{
		//	//oList.ShowData(false, true);
		//}

		//protected void btnInsert_Click(object sender, EventArgs e)
		//{
		//	Response.Redirect("Insert.aspx");
		//}

		//protected void btnHome_Click(object sender, EventArgs e)
		//{
		//	Response.Redirect("~/Default.aspx");
		//}

        #endregion
    }
}