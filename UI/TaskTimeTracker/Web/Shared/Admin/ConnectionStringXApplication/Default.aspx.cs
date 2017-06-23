using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using DataModel.Framework.Core;
using Framework.Components.ApplicationUser;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.ConnectionStringXApplication
{
	public partial class Default : Shared.UI.WebFramework.BasePage
	{

		#region Methods

		private List<ConnectionStringDataModel> GetConnectionStringList()
		{
			var dt = ConnectionStringDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedConnectionStrings(int applicationId)
		{
			var id = Convert.ToInt32(drpApplication.SelectedValue);
			var dt = ConnectionStringXApplicationDataManager.GetByApplication(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByApplication(int applicationId, List<int> connectionStringIds)
		{
			var id = Convert.ToInt32(drpApplication.SelectedValue);
			ConnectionStringXApplicationDataManager.DeleteByApplication(id, SessionVariables.RequestProfile);
			ConnectionStringXApplicationDataManager.CreateByApplication(id, connectionStringIds.ToArray(), SessionVariables.RequestProfile);
		}

		private List<ApplicationDataModel> GetApplicationList()
		{
			var dt = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedApplications(int connectionStringId)
		{
			var id = Convert.ToInt32(drpConnectionString.SelectedValue);
			var dt = ConnectionStringXApplicationDataManager.GetByConnectionString(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByConnectionString(int connectionStringId, List<int> applicationIds)
		{
			var id = Convert.ToInt32(drpConnectionString.SelectedValue);
			ConnectionStringXApplicationDataManager.DeleteByConnectionString(id, SessionVariables.RequestProfile);
			ConnectionStringXApplicationDataManager.CreateByConnectionString(id, applicationIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpApplication.DataSource = GetApplicationList();
			drpApplication.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpApplication.DataValueField = BaseDataModel.BaseDataColumns.ApplicationId;
			drpApplication.DataBind();

			drpConnectionString.DataSource = GetConnectionStringList();
			drpConnectionString.DataTextField = ConnectionStringDataModel.DataColumns.Name;
			drpConnectionString.DataValueField = ConnectionStringDataModel.DataColumns.ConnectionStringId;
			drpConnectionString.DataBind();
		}

		#endregion

		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			SettingCategory = "ConnectionStringXApplicationDefaultView";

			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();

		}

		protected override void OnInit(EventArgs e)
		{
			BindLists();

			BucketOfApplication.ConfigureBucket("Application", 1, GetApplicationList, GetAssociatedApplications, SaveByConnectionString);
			BucketOfConnectionString.ConfigureBucket("ConnectionString", 1, GetConnectionStringList, GetAssociatedConnectionStrings, SaveByApplication);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByConnectionString")
			{
				dynConnectionString.Visible = true;
				dynApplication.Visible = false;
				BucketOfApplication.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByApplication")
			{
				dynConnectionString.Visible = false;
				dynApplication.Visible = true;
				BucketOfConnectionString.ReloadBucketList();
			}
		}

		protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfConnectionString.ReloadBucketList();
		}

		protected void drpConnectionString_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfApplication.ReloadBucketList();
		}

		#endregion

	}
}