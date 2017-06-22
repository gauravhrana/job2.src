using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using System.Data;


namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{
		#region Private Methods
		private DataTable GetApplicationOperationData(int applicationId)
        {
            var data = new ApplicationOperationDataModel();

			var dt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetApplicationOperationColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ApplicationOperation, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetApplicationUserData(int applicationId)
        {
            var data = new ApplicationUserDataModel();
            //data.ApplicationId = applicationId;

			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetApplicationUserColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ApplicationUser, "DBColumns", SessionVariables.RequestProfile);
        }

		private DataTable GetApplicationUserData(string key)
		{
			return GetApplicationUserData(int.Parse(key));
		}

		private DataTable GetData(string key)
		{
			return GetApplicationOperationData(int.Parse(key));
		}

		protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();
			tabControl.AddTab("ApplicationRole", detailsControl, "Application Role", true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("ApplicationOperation", listControl);

            listControl.Setup("ApplicationOperation", "Shared/AuthenticationAndAuthorization", "ApplicationOperationId", setId, true, GetData, GetApplicationOperationColumns, "ApplicationOperation");
            listControl.SetSession("true");

            var listControlUser = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("ApplicationUser", listControlUser);

			listControlUser.Setup("ApplicationUser", "Shared/AuthenticationAndAuthorization", "ApplicationUserId", setId, true, GetApplicationUserData, GetApplicationUserColumns, "ApplicationUser");
            listControlUser.SetSession("true");

            tabControl.Setup("ApplicationRoleDetailsView"); 
                        

            return tabControl;
        }
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRole;
			PrimaryEntityKey = "ApplicationRole";
			DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationRole", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    }
}