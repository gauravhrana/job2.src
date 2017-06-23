using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.ReportCategory.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int reportCategoryId)
        {
            base.ShowData(reportCategoryId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new ReportCategoryDataModel();
            data.ReportCategoryId = reportCategoryId;

			var items = Framework.Components.Core.ReportCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];


                lblCreatedByAuditId.Text = item.CreatedByAuditId.ToString();
                lblModifiedByAuditId.Text = item.ModifiedByAuditId.ToString();
                lblCreatedDate.Text = item.CreatedDate.Value.ToString(SessionVariables.UserDateFormat);
                lblModifiedDate.Text = item.ModifiedDate.Value.ToString(SessionVariables.UserDateFormat);
                lblApplication.Text = item.Application;

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, reportCategoryId, "ReportCategory");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblReportCategoryIdText, lblNameText, lblDescriptionText, lblSortOrderText,
                lblCreatedByAuditIdText, lblCreatedDateText, lblModifiedDateText,  lblModifiedByAuditIdText });
            }
            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ReportCategoryDataModel();

            SetData(data);
        }

		public void SetData(ReportCategoryDataModel item)
        {
            SystemKeyId = item.ReportCategoryId;

            base.SetData(item);
        }
         
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.ReportCategoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReportCategory;

            PlaceHolderCore         = dynReportCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            MainTable               = tblMain1;
            BorderDiv               = borderdiv;

            CoreSystemKey           = lblReportCategoryId;
            CoreControlName         = lblName;
            CoreControlDescription  = lblDescription;
            CoreControlSortOrder    = lblSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblReportCategoryIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}