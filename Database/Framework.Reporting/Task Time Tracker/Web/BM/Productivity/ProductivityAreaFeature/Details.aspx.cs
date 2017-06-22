using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker;
using DataModel.Framework.AuthenticationAndAuthorization;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaFeature
{

    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        #region Methods

        private DataTable GetProductivityAreaFeatureXApplicationUserData(int productivityAreaFeatureId)
        {
			var dt = ProductivityAreaFeatureXApplicationUserDataManager.GetByProductivityAreaFeature(productivityAreaFeatureId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }


        private string[] GetProductivityAreaFeatureXApplicationUserColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ProductivityAreaFeatureXApplicationUser, "DBColumns", SessionVariables.RequestProfile);
        }

        private void SaveProductivityAreaFeatureXApplicationUser(int productivityAreaFeatureId, List<int> productivityAreaFeatureIds)
        {
			ProductivityAreaFeatureXApplicationUserDataManager.DeleteByProductivityAreaFeature(productivityAreaFeatureId, SessionVariables.RequestProfile.AuditId);
            ProductivityAreaFeatureXApplicationUserDataManager.CreateByProductivityAreaFeature(productivityAreaFeatureId, productivityAreaFeatureIds.ToArray(),SessionVariables.RequestProfile);
        }


        private DataTable GetProductivityAreaFeatureList()
        {
            var dt = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedProductivityAreaFeatures(int productivityAreaFeatureId)
        {
			var dt = ProductivityAreaFeatureXApplicationUserDataManager.GetByProductivityAreaFeature(productivityAreaFeatureId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        protected override Control GetTabControl(int setId, Control detailsControl)
        {


            var tabControl = ApplicationCommon.GetNewDetailTabControl();


            var bucketControlNeed = ApplicationCommon.GetNewBucketControl();
            bucketControlNeed.ConfigureBucket("ProductivityAreaFeature", setId, GetProductivityAreaFeatureList, GetAssociatedProductivityAreaFeatures, SaveProductivityAreaFeatureXApplicationUser);

            tabControl.Setup("ProductivityAreaFeatureDetailsView");
            tabControl.AddTab("ProductivityAreaFeature", detailsControl, String.Empty, true);
            tabControl.AddTab("ProductivityAreaFeatureXApplicationUser", bucketControlNeed);

            return tabControl;
        }

        #endregion


        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProductivityAreaFeature;
            PrimaryEntityKey = "ProductivityAreaFeature";
            DetailsControlPath = ApplicationCommon.GetControlPath("ProductivityAreaFeature", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }


        #endregion

    }
}
