using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.Report
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {


        #region Methods

        protected override Control GetTabControl(int setId, Control updateControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ReportUpdateView");

            var selected = false;

            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
            {
                selected = true;
            }

            tabControl.AddTab("Report", updateControl, String.Empty, selected);

            selected = false;

            if (Request.QueryString["tab"] == "2")
            {
                selected = true;
            }

            var bucketControl = ApplicationCommon.GetNewBucketControl();
            bucketControl.ConfigureBucket("ReportCategory", ApplicationCommon.GetSetId(), GetReportCategoryList, GetAssociatedReportXReportCategorys, SaveReportXReportCategory);
            tabControl.AddTab("ReportCategory", bucketControl, String.Empty, selected);

            return tabControl;
        }

       private DataTable GetReportCategoryList()
        {
			var dt = Framework.Components.Core.ReportCategoryDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedReportXReportCategorys(int reportId)
        {
			var dt = Framework.Components.Core.ReportXReportCategoryDataManager.GetByReport(reportId, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveReportXReportCategory(int reportId, List<int> reportXReportCategoryIds)
        {
			Framework.Components.Core.ReportXReportCategoryDataManager.DeleteByReport(reportId, SessionVariables.RequestProfile);
			Framework.Components.Core.ReportXReportCategoryDataManager.CreateByReport(reportId, reportXReportCategoryIds.ToArray(), SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Report;

            GenericControlPath = ApplicationCommon.GetControlPath("Report", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "Report";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;

        }

        #endregion Events
    }
}