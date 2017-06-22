using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker;
using DataModel.Framework.AuthenticationAndAuthorization;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaFeature
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
    
        #region Methods       


        protected override Control GetTabControl(int setId, Control updateControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();                     

            tabControl.Setup("ProductivityAreaFeatureUpdateView");

			var selected = false;
			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}

            tabControl.AddTab("Productivity Area Feature", updateControl, String.Empty, true);

			selected = false;
			if (Request.QueryString["tab"] == "2")
			{
				selected = true;
			}

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("Productivity Area", listControl);

			listControl.Setup("ProductivityArea", "ProductivityArea", "ProductivityAreaId", setId, true, GetData, GetProductivityAreaXProductivityAreaFeatureColumns, "ProductivityAreaXProductivityAreaFeature");
            listControl.SetSession("true");

            var bucketControlApplicationUser = ApplicationCommon.GetNewBucketControl();
			bucketControlApplicationUser.ConfigureBucket("ApplicationUser", setId, GetApplicationUserList, GetAssociatedProductivityAreaFeatures, SaveProductivityAreaFeatureXApplicationUser);
             
			if (Request.QueryString["tab"] == "3")
			{
				selected = true;
			}

			tabControl.AddTab("Application User", bucketControlApplicationUser, String.Empty, selected);
            return tabControl;
        }

        private DataTable GetApplicationUserList()
        {
			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedProductivityAreaFeatures(int productivityAreaFeatureId)
        {
			var dt = ProductivityAreaFeatureXApplicationUserDataManager.GetByProductivityAreaFeature(productivityAreaFeatureId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private void SaveProductivityAreaFeatureXApplicationUser(int productivityAreaFeatureId, List<int> applicationUserIds)
        {
			ProductivityAreaFeatureXApplicationUserDataManager.DeleteByProductivityAreaFeature(productivityAreaFeatureId, SessionVariables.RequestProfile.AuditId);
            ProductivityAreaFeatureXApplicationUserDataManager.CreateByProductivityAreaFeature(productivityAreaFeatureId, applicationUserIds.ToArray(), SessionVariables.RequestProfile);
        }

		private DataTable GetData(string key)
		{
			return GetProductivityAreaXProductivityAreaFeatureData(int.Parse(key));
		}

        private DataTable GetProductivityAreaXProductivityAreaFeatureData(int productivityAreaId)
        {
			var dt = ProductivityAreaFeatureXApplicationUserDataManager.GetByProductivityAreaFeature(productivityAreaId, SessionVariables.RequestProfile.AuditId);
           
            return dt;
        } 

        private string[] GetProductivityAreaXProductivityAreaFeatureColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ProductivityAreaXProductivityAreaFeature, "DBColumns", SessionVariables.RequestProfile);
        }           

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProductivityAreaFeature;

            GenericControlPath = ApplicationCommon.GetControlPath("ProductivityAreaFeature", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "ProductivityAreaFeature";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;

        }

        #endregion

    }
}