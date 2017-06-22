using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRoleMapping
{
    public partial class PersonView : Shared.UI.WebFramework.BasePage
    {

        #region Methods

        private List<ApplicationUserDataModel> GetApplicationUserList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedApplicationUsers(int applicationRoleId)
        {
            var id = Convert.ToInt32(drpApplicationRole.SelectedValue);
			var dt = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationRole(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByApplicationRole(int applicationRoleId, List<int> applicationUserIds)
        {
            var id = Convert.ToInt32(drpApplicationRole.SelectedValue);
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.DeleteByApplicationRole(id, SessionVariables.RequestProfile);
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationRole(id, applicationUserIds.ToArray(), SessionVariables.RequestProfile);
        }

        private List<ApplicationRoleDataModel> GetApplicationRoleList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedApplicationRoles(int ApplicationUserId)
        {
            var id = Convert.ToInt32(drpApplicationUser.SelectedValue);
			var dt = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByApplicationUser(int applicationUserId, List<int> applicationRoleIds)
        {
            var id = Convert.ToInt32(drpApplicationUser.SelectedValue);
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.DeleteByApplicationUser(id, SessionVariables.RequestProfile);
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationUser(id, applicationRoleIds.ToArray(), SessionVariables.RequestProfile);
        }

        private void BindLists()
        {
            drpApplicationUser.DataSource = GetApplicationUserList();
            drpApplicationUser.DataTextField = ApplicationUserDataModel.DataColumns.FirstName;
            drpApplicationUser.DataValueField = ApplicationUserDataModel.DataColumns.ApplicationUserId;
            drpApplicationUser.DataBind();

            drpApplicationRole.DataSource = GetApplicationRoleList();
            drpApplicationRole.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpApplicationRole.DataValueField = ApplicationRoleDataModel.DataColumns.ApplicationRoleId;
            drpApplicationRole.DataBind();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            BindLists();

            BucketOfApplicationRole.ConfigureBucket("ApplicationRole", 1, GetApplicationRoleList, GetAssociatedApplicationRoles, SaveByApplicationUser);
            BucketOfApplicationUser.ConfigureBucket("ApplicationUser", 1, GetApplicationUserList, GetAssociatedApplicationUsers, SaveByApplicationRole);
        }

        protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSelection.SelectedValue == "ByApplicationUser")
            {
                dynApplicationUser.Visible = true;
                dynApplicationRole.Visible = false;
                BucketOfApplicationRole.ReloadBucketList();
            }
            else if (drpSelection.SelectedValue == "ByApplicationRole")
            {
                dynApplicationUser.Visible = false;
                dynApplicationRole.Visible = true;
                BucketOfApplicationUser.ReloadBucketList();
            }
        }

        protected void drpApplicationRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfApplicationUser.ReloadBucketList();
        }

        protected void drpApplicationUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfApplicationRole.ReloadBucketList();
        }

        #endregion

    }
}