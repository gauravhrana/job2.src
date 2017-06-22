using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaXProductivityAreaFeature
{
    public partial class CrossReference : BasePage
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
			var dt = ProductivityAreaXProductivityAreaFeatureDataManager.GetByProductivityAreaFeature(id, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private void SaveByProductivityAreaFeature(int ProductivityAreaFeatureId, List<int> productivityAreaIds)
        {
            var id = Convert.ToInt32(drpProductivityAreaFeature.SelectedValue);
			ProductivityAreaXProductivityAreaFeatureDataManager.DeleteByProductivityAreaFeature(id, SessionVariables.RequestProfile.AuditId);
            ProductivityAreaXProductivityAreaFeatureDataManager.CreateByProductivityAreaFeature(id, productivityAreaIds.ToArray(), SessionVariables.RequestProfile);
        }

        private DataTable GetProductivityAreaList()
        {
            var dt = ProductivityAreaDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedProductivityAreas(int productivityAreaFeatureId)
        {
            var id = Convert.ToInt32(drpProductivityArea.SelectedValue);
			var dt = ProductivityAreaXProductivityAreaFeatureDataManager.GetByProductivityArea(id, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private void SaveByProductivityArea(int productivityAreaId, List<int> productivityAreaFeatureIds)
        {
            var id = Convert.ToInt32(drpProductivityArea.SelectedValue);
			ProductivityAreaXProductivityAreaFeatureDataManager.DeleteByProductivityArea(id, SessionVariables.RequestProfile.AuditId);
            ProductivityAreaXProductivityAreaFeatureDataManager.Create(id, productivityAreaFeatureIds.ToArray(), SessionVariables.RequestProfile);
        }

        private void BindLists()
        {
            drpProductivityArea.DataSource = GetProductivityAreaList();
            drpProductivityArea.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpProductivityArea.DataValueField = ProductivityAreaDataModel.DataColumns.ProductivityAreaId;
            drpProductivityArea.DataBind();

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

            BucketOfProductivityArea.ConfigureBucket("ProductivityArea", 1, GetProductivityAreaList, GetAssociatedProductivityAreas, SaveByProductivityAreaFeature);
            BucketOfProductivityAreaFeature.ConfigureBucket("ProductivityAreaFeature", 1, GetProductivityAreaFeatureList, GetAssociatedProductivityAreaFeatures, SaveByProductivityArea);
        }

        protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSelection.SelectedValue == "ByProductivityAreaFeature")
            {
                dynProductivityAreaFeature.Visible = true;
                dynProductivityArea.Visible = false;
                BucketOfProductivityArea.ReloadBucketList();
            }
            else if (drpSelection.SelectedValue == "ByProductivityArea")
            {
                dynProductivityAreaFeature.Visible = false;
                dynProductivityArea.Visible = true;
                BucketOfProductivityAreaFeature.ReloadBucketList();
            }
        }

        protected void drpProductivityArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfProductivityAreaFeature.ReloadBucketList();
        }

        protected void drpProductivityAreaFeature_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfProductivityArea.ReloadBucketList();
        }

        #endregion

    }
}