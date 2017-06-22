using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaFeatureXApplicationUser
{
	public partial class CrossReference : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Methods

        private DataTable GetProductivityAreaFeatureList()
        {
            var dt = ProductivityAreaFeatureDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedProductivityAreaFeatures(int productivityAreaFeatureId)
        {
            var id = Convert.ToInt32(drpProductivityAreaFeature.SelectedValue);
			var dt = ProductivityAreaFeatureXApplicationUserDataManager.GetByProductivityAreaFeature(id, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private void SaveByProductivityAreaFeature(int ProductivityAreaFeatureId, List<int> applicationUserIds)
        {
            var id = Convert.ToInt32(drpProductivityAreaFeature.SelectedValue);
			ProductivityAreaFeatureXApplicationUserDataManager.DeleteByProductivityAreaFeature(id, SessionVariables.RequestProfile.AuditId);
            ProductivityAreaFeatureXApplicationUserDataManager.CreateByProductivityAreaFeature(id, applicationUserIds.ToArray(), SessionVariables.RequestProfile);
        }

        private DataTable GetApplicationUserList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedApplicationUsers(int applicationUserFeatureId)
        {
            var id = Convert.ToInt32(drpApplicationUser.SelectedValue);
			var dt = ProductivityAreaFeatureXApplicationUserDataManager.GetByApplicationUser(id, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private void SaveByApplicationUser(int applicationUserId, List<int> productivityAreaFeatureIds)
        {
            var id = Convert.ToInt32(drpApplicationUser.SelectedValue);
			ProductivityAreaFeatureXApplicationUserDataManager.DeleteByApplicationUser(id, SessionVariables.RequestProfile.AuditId);
            ProductivityAreaFeatureXApplicationUserDataManager.Create(id, productivityAreaFeatureIds.ToArray(), SessionVariables.RequestProfile);
        }

        private void BindLists()
        {
            drpApplicationUser.DataSource = GetApplicationUserList();
            drpApplicationUser.DataTextField = ApplicationUserDataModel.DataColumns.LastName;
            drpApplicationUser.DataValueField = ApplicationUserDataModel.DataColumns.ApplicationUserId;
            drpApplicationUser.DataBind();

            drpProductivityAreaFeature.DataSource = GetProductivityAreaFeatureList();
			drpProductivityAreaFeature.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpProductivityAreaFeature.DataValueField = ProductivityAreaFeatureDataModel.DataColumns.ProductivityAreaFeatureId;
            drpProductivityAreaFeature.DataBind();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            BindLists();

            BucketOfApplicationUser.ConfigureBucket("ApplicationUser", 1, GetApplicationUserList, GetAssociatedApplicationUsers, SaveByProductivityAreaFeature);
            BucketOfProductivityAreaFeature.ConfigureBucket("ProductivityAreaFeature", 1, GetProductivityAreaFeatureList, GetAssociatedProductivityAreaFeatures, SaveByApplicationUser);
        }

        protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSelection.SelectedValue == "ByProductivityAreaFeature")
            {
                dynProductivityAreaFeature.Visible = true;
                dynApplicationUser.Visible = false;
                BucketOfApplicationUser.ReloadBucketList();
            }
            else if (drpSelection.SelectedValue == "ByApplicationUser")
            {
                dynProductivityAreaFeature.Visible = false;
                dynApplicationUser.Visible = true;
                BucketOfProductivityAreaFeature.ReloadBucketList();
            }
        }

        protected void drpApplicationUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfProductivityAreaFeature.ReloadBucketList();
        }

        protected void drpProductivityAreaFeature_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfApplicationUser.ReloadBucketList();
        }

        #endregion

    }
}