using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.Core;

namespace ApplicationContainer.UI.Web.ReportCategory
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate

    {
        #region Methods

        protected override Control GetTabControl(int setId, Control updateControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ReportCategoryUpdateView");

            var selected = false;

            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
            {
                selected = true;
            }

            tabControl.AddTab("ReportCategory", updateControl, String.Empty, selected);

            selected = false;

            if (Request.QueryString["tab"] == "2")
            {
                selected = true;
            }

            var bucketControl = ApplicationCommon.GetNewBucketControl();
            bucketControl.ConfigureBucket("Report", setId, GetReportXReportCategoryList, GetAssociatedReportXReportCategorys, SaveReportXReportCategory);
            tabControl.AddTab("Report", bucketControl, String.Empty, selected);

            return tabControl;
        }

        private List<ReportDataModel> GetReportXReportCategoryList()
        {
			var dt = Framework.Components.Core.ReportDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedReportXReportCategorys(int reportCategoryId)
        {
			var dt = Framework.Components.Core.ReportXReportCategoryDataManager.GetByReportCategory(reportCategoryId, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveReportXReportCategory(int reportCategoryId, List<int> reportIds)
        {
			Framework.Components.Core.ReportXReportCategoryDataManager.DeleteByReportCategory(reportCategoryId, SessionVariables.RequestProfile);
			Framework.Components.Core.ReportXReportCategoryDataManager.CreateByReportCategory(reportCategoryId, reportIds.ToArray(), SessionVariables.RequestProfile);
        }

        #endregion

        #region

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReportCategory;

            GenericControlPath = ApplicationCommon.GetControlPath("ReportCategory", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "ReportCategory";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;

        }
        #endregion
    }
}