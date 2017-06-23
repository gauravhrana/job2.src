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

namespace Shared.UI.Web.ApplicationManagement.Development.TestData
{
	public partial class EntityNextValueData : Framework.UI.Web.BaseClasses.PageBasePage
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

		private void DataBind(int applicationId)
		{
			var dt = Framework.Components.Core.SystemEntityTypeDataManager.EntityIncorrectNextValueData(SessionVariables.RequestProfile, applicationId);

			DataGrid.DataSource = dt;
			DataGrid.DataBind();
		}
		#endregion

		#region Events
		
		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "AuditHistoryDefaultView";
			DetailUserPreferenceCategoryId = PreferenceUtility.CreateUserPreferenceCategoryIfNotExists("AuditHistory", "AuditHistory");

			//oSearchFilter.SearchControl.SettingCategory = SettingCategory + "SearchControl";

			//oSearchFilter.GetFilter(SystemEntity.AuditHistory, "AuditHistoryId");

			//SettingCategory = "EntityTestDataDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;

		}

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				var appData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(appData, ddlApplication, DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.Name,
					DataModel.Framework.AuthenticationAndAuthorization.ApplicationDataModel.DataColumns.ApplicationId);

				ddlApplication.Items.Insert(0, new ListItem("All", "-1"));

				//DataBind(-1); 
			}

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var appId = Convert.ToInt32(ddlApplication.SelectedValue.ToString());

			if (appId != -1)
				DataBind(appId);
			else
				DataBind();
		}
		#endregion

	}
}