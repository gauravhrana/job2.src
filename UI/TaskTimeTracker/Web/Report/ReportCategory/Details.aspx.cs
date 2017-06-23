using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.Core;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ReportCategory
{
    public partial class Details : PageDetails
    {
        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ReportCategoryDetailsView");

            tabControl.AddTab("ReportCategory", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("ReportXReportCategory", listControl);

			listControl.Setup("ReportXReportCategory", String.Empty, "ReportXReportCategoryId", setId, true, GetData, GetReportXReportCategoryColumns, "ReportCategory");
            listControl.SetSession("true");

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetReportXReportCategoryData(int.Parse(key));
		}

        private Shared.UI.Web.Controls.DetailTab1Control GetVerticalTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetail1TabControl();

            tabControl.AddTab("ReportCategory", detailsControl, "ReportCategory", false);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("ReportXReportCategory", listControl, "ReportXReportCategory");

            listControl.Setup("ReportXReportCategory", String.Empty, "ReportXReportCategoryId", setId, true, GetData, GetReportXReportCategoryColumns, "ReportXReportCategory");
            listControl.SetSession("true");

            return tabControl;
        }

        private DataTable GetReportXReportCategoryData(int reportCategoryId)
        {
			var dt = Framework.Components.Core.ReportXReportCategoryDataManager.GetByReportCategory(reportCategoryId, SessionVariables.RequestProfile);
			var reportXReportCategorydt = Framework.Components.Core.ReportXReportCategoryDataManager.Search(
                ReportXReportCategoryDataModel.Empty, SessionVariables.RequestProfile);
            var resultdt = reportXReportCategorydt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                var rows = reportXReportCategorydt.Select("reportXReportCategoryId = " + row[ReportXReportCategoryDataModel.DataColumns.ReportXReportCategoryId]);
                resultdt.ImportRow(rows[0]);
            }

            return resultdt;
        }

        private string[] GetReportXReportCategoryColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReportXReportCategory, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntity		   = SystemEntity.ReportCategory;
	        PrimaryEntityKey	   = "ReportCategory";
            DetailsControlPath	   = ApplicationCommon.GetControlPath("ReportCategory", ControlType.DetailsControl);
            PrimaryPlaceHolder     = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject	   = Master.BreadCrumbObject;
        }

        #endregion

    }
}
