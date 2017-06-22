using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        private System.Data.DataTable GetProfileImageData(int ApplicationUserid)
        {
            var data = new ApplicationUserProfileImageDataModel();
            data.ApplicationUserId = ApplicationUserid;

			var dt = Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetProfileImageColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetApplicationRuleData(int applicationUserId)
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(applicationUserId, SessionVariables.RequestProfile);
			var appRoledt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = appRoledt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = appRoledt.Select("ApplicationRoleId = " + row[ApplicationRoleDataModel.DataColumns.ApplicationRoleId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
        }

        private string[] GetApplicationRuleColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ApplicationRole, "DBColumns", SessionVariables.RequestProfile);
        }

		protected override Control GetTabControl(int setId, Control detailsControl)
        {
			var tabControl = ApplicationCommon.GetNewDetailTabControl();
			tabControl.Setup("ApplicationUserDetailsView");   
			
			tabControl.AddTab("ApplicationUser", detailsControl, String.Empty, true);

            var activityStreamPath = "~/Shared/Controls/ActivityStream.ascx";
			var activityStream = (Shared.UI.Web.Controls.ActivityStreamControl)Page.LoadControl(activityStreamPath);
			activityStream.ActivityStreamName = "activity_user_detail_" + setId.ToString().Replace("-", "_");
			activityStream.ActivityStreamAuditId = setId;			

			tabControl.AddTab("ActivityStream", activityStream, "User Activity");

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			listControl.Setup("ApplicationRole", "", "ApplicationRoleId", setId, true, GetData, GetApplicationRuleColumns, "ApplicationRole");
			listControl.SetSession("true");
			
			tabControl.AddTab("ApplicationRole", listControl);

			var listControlAUPI = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControlAUPI.Setup("ApplicationUserProfileImage", "", "ApplicationUserProfileImageId", setId, true, GetProfileImageData, GetProfileImageColumns, "ApplicationUserProfileImage");
			listControlAUPI.SetSession("true");

			tabControl.AddTab("ApplicationUserProfileImage", listControlAUPI, "ProfileImage");

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetApplicationRuleData(int.Parse(key));
		}

		private DataTable GetProfileImageData(string key)
		{
			return GetProfileImageData(int.Parse(key));
		}

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.ApplicationUser;
			PrimaryEntityKey	= "ApplicationUser";
			DetailsControlPath	= ApplicationCommon.GetControlPath("ApplicationUser", ControlType.DetailsControl);
			PrimaryPlaceHolder	= plcDetailsList;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    }
}