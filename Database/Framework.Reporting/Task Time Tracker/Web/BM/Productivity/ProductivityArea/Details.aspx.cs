using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityArea
{

    public partial class Details : PageDetails
    {
        #region Methods        

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("ProductivityAreaFeature", String.Empty, "ProductivityAreaFeatureId", setId, true, GetData, GetProductivityAreaFeatureColumns, "ProductivityAreaFeature");
            listControl.SetSession("true");           

            tabControl.Setup("ProductivityAreaFeatureDetailsView");
            tabControl.AddTab("Productivity Area", detailsControl, String.Empty, true);
            tabControl.AddTab("Productivity Area Feature", listControl);
           
            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetProductivityAreaFeatureData(int.Parse(key));
		}

        private DataTable GetProductivityAreaFeatureData(int productivityAreaId)
        {
            var data = new ProductivityAreaFeatureDataModel();
            data.ProductivityAreaFeatureId = productivityAreaId;
            var dt = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }
      
        private string[] GetProductivityAreaFeatureColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ProductivityAreaFeature, "DBColumns", SessionVariables.RequestProfile);
        }     


        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity        = SystemEntity.ProductivityArea;
            PrimaryEntityKey     = "ProductivityArea";
            DetailsControlPath   = ApplicationCommon.GetControlPath("ProductivityArea", ControlType.DetailsControl);
            PrimaryPlaceHolder   = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject     = Master.BreadCrumbObject;
        }

        #endregion

    }
}
