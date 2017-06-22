using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region variables

        private List<ApplicationRoleDataModel> GetAppRoleList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private System.Data.DataTable GetAssociatedApplicationRoleData(int ApplicationUserid)
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(ApplicationUserid, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveApplicationUserXApplicationRole(int applicationId, List<int> ProjectIds)
        {
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(applicationId, SessionVariables.RequestProfile);
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationUser(applicationId, ProjectIds.ToArray(), SessionVariables.RequestProfile);
        }

        private System.Data.DataTable GetProfileImageData(int ApplicationUserid)
        {
            var data               = new ApplicationUserProfileImageDataModel();
            data.ApplicationUserId = ApplicationUserid;
			var dt = Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetProfileImageColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage, "DBColumns", SessionVariables.RequestProfile);
        }

		protected override Control GetTabControl(int setId, Control detailsControl)
        {
			var tabControl = ApplicationCommon.GetNewDetailTabControl();
			tabControl.Setup("ApplicationUserUpdateView"); 

			tabControl.AddTab("ApplicationUser", detailsControl, "Application User", true);
            
            var activityStreamPath = "~/Shared/Controls/ActivityStream.ascx";
			var activityStream = (Shared.UI.Web.Controls.ActivityStreamControl)Page.LoadControl(activityStreamPath);

			activityStream.ActivityStreamName = "activity_user_detail_" + setId.ToString().Replace("-", "_");
			activityStream.ActivityStreamAuditId = setId;

			tabControl.AddTab("ActivityStream", activityStream, "User Activity Stream");
			//tabControl.Controls.Add(new LiteralControl("<br />"));

            var listControlAUPI = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("ApplicationUserProfileImage", listControlAUPI, "Profile Image");

			listControlAUPI.Setup("ApplicationUserProfileImage", "", "ApplicationUserProfileImageId", setId, true, GetData, GetProfileImageColumns, "ApplicationUserProfileImage");
            listControlAUPI.SetSession("true");

			var bucketControl = ApplicationCommon.GetNewBucketControl();
			bucketControl.ConfigureBucket("ApplicationRole", setId, GetAppRoleList, GetAssociatedApplicationRoleData, SaveApplicationUserXApplicationRole);

			tabControl.AddTab("ApplicationRole", bucketControl, "Application Role");
            

            return tabControl;
        }

		private System.Data.DataTable GetData(string key)
		{
			return GetProfileImageData(int.Parse(key));
		}

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity       = Framework.Components.DataAccess.SystemEntity.ApplicationUser;

			GenericControlPath	= ApplicationCommon.GetControlPath("ApplicationUser", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "ApplicationUser";
			BreadCrumbObject	= Master.BreadCrumbObject;

			BtnUpdate	        = btnUpdate;
			BtnClone	        = btnClone;
			BtnCancel	        = btnCancel;
		}

		#endregion

    }
}