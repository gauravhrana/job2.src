using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.Core;

namespace ApplicationContainer.UI.Web.Report 
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ReportDetailsView");

            tabControl.AddTab("Report", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("ReportXReportCategory", listControl);

            listControl.Setup("ReportXReportCategory", String.Empty, "ReportXReportCategoryId", setId, true, GetData, GetReportXReportCategoryColumns, "Report");
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

            tabControl.AddTab("Report", detailsControl, "Report", false);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("ReportXReportCategory", listControl, "ReportXReportCategory");

            listControl.Setup("ReportXReportCategory", String.Empty, "ReportXReportCategoryId", setId, true, GetData, GetReportXReportCategoryColumns, "ReportXReportCategory");
            listControl.SetSession("true");

            return tabControl;
        }

        private DataTable GetReportXReportCategoryData(int reportId)
        {
			var dt = Framework.Components.Core.ReportXReportCategoryDataManager.GetByReport(reportId, SessionVariables.RequestProfile);
			var reportXReportCategorydt = Framework.Components.Core.ReportXReportCategoryDataManager.GetList(SessionVariables.RequestProfile);
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

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Report;
            PrimaryEntityKey = "Report";
            DetailsControlPath = ApplicationCommon.GetControlPath("Report", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
        }
       
        #endregion

    }
}
