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
	public partial class ElapsedTimeData : Framework.UI.Web.BaseClasses.PageBasePage
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

		public string ComputerName
		{
			get
			{
				return txtComputerNamee.Text;
			}
		}

		public string ConnectionKey
		{
			get
			{
				return txtConnectionKey.Text;
			}
		}

		#region private methods

		private void DataBind()
		{
			var data = new Framework.Components.LogAndTrace.Log4NetDataModel();

			data.Computer		= ComputerName;
			data.ConnectionKey	= ConnectionKey;

			var dt = Framework.Components.LogAndTrace.Log4NetDataManager.GetElapsedTimeRecords(data, SessionVariables.RequestProfile);
			
			DataGrid.DataSource = dt;
			DataGrid.DataBind();
		}
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "AuditHistoryDefaultView";
			DetailUserPreferenceCategoryId = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists("AuditHistory", "AuditHistory");

			//oSearchFilter.SearchControl.SettingCategory = SettingCategory + "SearchControl";

			//oSearchFilter.GetFilter(SystemEntity.AuditHistory, "AuditHistoryId");

			//SettingCategory = "EntityTestDataDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			DataBind();
		}
		#endregion

	}
}