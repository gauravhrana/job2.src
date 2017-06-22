using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityArea
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {


        #region Methods       


        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ProductivityAreaUpdateView");

            tabControl.AddTab("Productivity Area", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("Productivity Area Feature", listControl);

			listControl.Setup("ProductivityAreaXProductivityAreaFeature", "ProductivityAreaXProductivityAreaFeature", "ProductivityAreaXProductivityAreaFeatureId", setId, true, GetData, GetProductivityAreaXProductivityAreaFeatureColumns, "ProductivityAreaXProductivityAreaFeature");
            listControl.SetSession("true"); 


            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetProductivityAreaXProductivityAreaFeatureData(int.Parse(key));
		}

        private DataTable GetProductivityAreaXProductivityAreaFeatureData(int productivityAreaId)
        {
			var dt = ProductivityAreaXProductivityAreaFeatureDataManager.GetByProductivityArea(productivityAreaId, SessionVariables.RequestProfile.AuditId);
            //var fdt = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeature.GetList(AuditId);
            //var resultdt = fdt.Clone();
            //foreach (DataRow row in dt.Rows)
            //{
            //    var rows = fdt.Select("ProductivityAreaId = " + row[ProductivityAreaDataModel.DataColumns.ProductivityAreaId]);
            //    resultdt.ImportRow(rows[0]);
            //}
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

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProductivityArea;

            GenericControlPath = ApplicationCommon.GetControlPath("ProductivityArea", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "ProductivityArea";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;

        }

        #endregion

    }
}