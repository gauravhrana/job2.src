using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ReportXReportCategory
{
    public partial class CrossReference : Shared.UI.WebFramework.BasePage
    {
        #region Methods

        private List<ReportCategoryDataModel> GetReportCategoryList()
        {
            var dt = Framework.Components.Core.ReportCategoryDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedReportCategorys(int ReportId)
        {
            var id = Convert.ToInt32(drpReport.SelectedValue);
			var dt = Framework.Components.Core.ReportXReportCategoryDataManager.GetByReport(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByReport(int ReportId, List<int> ReportCategoryIds)
        {
            var id = Convert.ToInt32(drpReport.SelectedValue);
			Framework.Components.Core.ReportXReportCategoryDataManager.DeleteByReport(id, SessionVariables.RequestProfile);
			Framework.Components.Core.ReportXReportCategoryDataManager.CreateByReport(id, ReportCategoryIds.ToArray(), SessionVariables.RequestProfile);
        }

        private List<ReportDataModel> GetReportList()
        {
			var dt = Framework.Components.Core.ReportDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedReports(int ReportCategoryId)
        {
            var id = Convert.ToInt32(drpReportCategory.SelectedValue);
			var dt = Framework.Components.Core.ReportXReportCategoryDataManager.GetByReportCategory(id, SessionVariables.RequestProfile);
            return dt;
        }

        private void SaveByReportCategory(int ReportCategoryId, List<int> ReportIds)
        {
            var id = Convert.ToInt32(drpReportCategory.SelectedValue);
			Framework.Components.Core.ReportXReportCategoryDataManager.DeleteByReportCategory(id, SessionVariables.RequestProfile);
			Framework.Components.Core.ReportXReportCategoryDataManager.CreateByReportCategory(id, ReportIds.ToArray(), SessionVariables.RequestProfile);
        }

        private void BindLists()
        {
            drpReport.DataSource = GetReportList();
            drpReport.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpReport.DataValueField = ReportDataModel.DataColumns.ReportId;
            drpReport.DataBind();

            drpReportCategory.DataSource = GetReportCategoryList();
            drpReportCategory.DataTextField = StandardDataModel.StandardDataColumns.Name;
            drpReportCategory.DataValueField = ReportCategoryDataModel.DataColumns.ReportCategoryId;
            drpReportCategory.DataBind();
        }

        #endregion

        #region Events

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);


            SettingCategory = "ReportXReportCategoryDefaultView";

        }

        protected override void OnInit(EventArgs e)
        {

            BindLists();

            BucketOfReport.ConfigureBucket("Report", 1, GetReportList, GetAssociatedReports, SaveByReportCategory);
            BucketOfReportCategory.ConfigureBucket("ReportCategory", 1, GetReportCategoryList, GetAssociatedReportCategorys, SaveByReport);
        }

        protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSelection.SelectedValue == "ByReportCategory")
            {
                dynReportCategory.Visible = true;
                dynReport.Visible = false;
                BucketOfReport.ReloadBucketList();
            }
            else if (drpSelection.SelectedValue == "ByReport")
            {
                dynReportCategory.Visible = false;
                dynReport.Visible = true;
                BucketOfReportCategory.ReloadBucketList();
            }
        }

        protected void drpReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfReportCategory.ReloadBucketList();
        }

        protected void drpReportCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BucketOfReport.ReloadBucketList();
        }

        #endregion
    }
}