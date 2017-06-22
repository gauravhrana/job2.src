using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
        #region Methods

        private DataTable GetApplicationOperationList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedApplicationOperations(int applicationRoleId)
        {
			var dt = Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.GetByApplicationRole(applicationRoleId, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveApplicationOperationXApplicationRole(int applicationRoleId, List<int> applicationOperationIds)
        {
			Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.DeleteByApplicationRole(applicationRoleId, SessionVariables.RequestProfile);
			Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.CreateByApplicationRole(applicationRoleId, applicationOperationIds.ToArray(), SessionVariables.RequestProfile);
        }

        private DataTable GetApplicationUserList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedApplicationUsers(int applicationRoleId)
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationRole(applicationRoleId, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveApplicationUserXApplicationRole(int applicationRoleId, List<int> applicationUserIds)
        {
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.DeleteByApplicationRole(applicationRoleId, SessionVariables.RequestProfile);
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationRole(applicationRoleId, applicationUserIds.ToArray(), SessionVariables.RequestProfile);
        }

		protected override Control GetTabControl(int setId, Control detailsControl)
		{	

			var tabControl = ApplicationCommon.GetNewDetailTabControl();
			tabControl.Setup("ApplicationRoleUpdateView");
			var selected = false;
			tabControl.AddTab("ApplicationRole", detailsControl, "Application Role", selected);	

			
			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}
			
			selected = false;
			if (Request.QueryString["tab"] == "2")
			{
				selected = true;
			}

			var bucketControl = ApplicationCommon.GetNewBucketControl();


			bucketControl.ConfigureBucket("ApplicationOperation", ApplicationCommon.GetSetId(), GetApplicationOperationList, GetAssociatedApplicationOperations, SaveApplicationOperationXApplicationRole);

			//tabControl.AddTab("ApplicationOperation", String.Empty, selected, bucketControl();			
			selected = false;
			if (Request.QueryString["tab"] == "3")
			{
				selected = true;
			}

			var bucketControl1 = ApplicationCommon.GetNewBucketControl();

			bucketControl1.ConfigureBucket("ApplicationUser", SetId, GetApplicationUserList, GetAssociatedApplicationUsers, SaveApplicationUserXApplicationRole);
			

			
			tabControl.AddTab("ApplicationUser", bucketControl1, "Application User", selected);
			tabControl.AddTab("ApplicationOperation", bucketControl, "Application Operation", selected);
			return tabControl;

		}

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ApplicationRole;

            GenericControlPath   = ApplicationCommon.GetControlPath("ApplicationRole", ControlType.GenericControl);
            PrimaryPlaceHolder   = plcUpdateList;
            PrimaryEntityKey     = "ApplicationRole";
            BreadCrumbObject     = Master.BreadCrumbObject;

            BtnUpdate            = btnUpdate;
            BtnClone             = btnClone;
            BtnCancel            = btnCancel;
        }

        #endregion

    }
}