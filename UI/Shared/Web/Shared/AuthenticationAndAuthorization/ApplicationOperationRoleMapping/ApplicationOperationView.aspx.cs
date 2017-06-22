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

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperationRoleMapping
{
    public partial class ApplicationOperationView : Shared.UI.WebFramework.BasePage
    {
        #region Methods

        private DataTable GetApplicationOperationList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedApplicationOperations(int applicationRoleId)
        {
            var id = Convert.ToInt32(drpApplicationRole.SelectedValue);
			var dt = Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.GetByApplicationRole(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByApplicationRole(int applicationRoleId, List<int> ApplicationOperationIds)
        {
            var id = Convert.ToInt32(drpApplicationRole.SelectedValue);
			Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.DeleteByApplicationRole(id, SessionVariables.RequestProfile);
			Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.CreateByApplicationRole(id, ApplicationOperationIds.ToArray(), SessionVariables.RequestProfile);
        }

        private DataTable GetApplicationRoleList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedApplicationRoles(int applicationOperationId)
        {
            var id = Convert.ToInt32(drpApplicationOperation.SelectedValue);
			var dt = Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.GetByApplicationOperation(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByApplicationOperation(int applicationOperationId, List<int> applicationRoleIds)
        {
            var id = Convert.ToInt32(drpApplicationOperation.SelectedValue);
			Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.DeleteByApplicationOperation(id, SessionVariables.RequestProfile);
			Framework.Components.ApplicationUser.ApplicationOperationXApplicationRoleDataManager.Create(id, applicationRoleIds.ToArray(), SessionVariables.RequestProfile);
        }

        private void BindLists()
        {
            drpApplicationOperation.DataSource = GetApplicationOperationList();
            drpApplicationOperation.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpApplicationOperation.DataValueField = ApplicationOperationDataModel.DataColumns.ApplicationOperationId;
            drpApplicationOperation.DataBind();

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

            BucketOfApplicationRole.ConfigureBucket("ApplicationRole", 1, GetApplicationRoleList, GetAssociatedApplicationRoles, SaveByApplicationOperation);
            BucketOfApplicationOperation.ConfigureBucket("ApplicationOperation", 1, GetApplicationOperationList, GetAssociatedApplicationOperations, SaveByApplicationRole);
        }

        protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSelection.SelectedValue == "ByApplicationOperation")
            {
                dynApplicationOperation.Visible = true;
                dynApplicationRole.Visible = false;
                BucketOfApplicationRole.ReloadBucketList();
            }
            else if (drpSelection.SelectedValue == "ByApplicationRole")
            {
                dynApplicationOperation.Visible = false;
                dynApplicationRole.Visible = true;
                BucketOfApplicationOperation.ReloadBucketList();
            }
        }

        protected void drpApplicationRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfApplicationOperation.ReloadBucketList();
        }

        protected void drpApplicationOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfApplicationRole.ReloadBucketList();
        }

        #endregion

    }
}