using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.Report.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int reportId)
        {
            base.ShowData(reportId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new ReportDataModel();
            data.ReportId = reportId;

			var items = Framework.Components.Core.ReportDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

				lblApplication.Text=item.Application;
                
                lblTitle.Text = item.Title;              
                				
                SetData(item);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, reportId, "Report");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblReportIdText, lblTitleText, lblNameText, lblDescriptionText, lblSortOrderText});
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ReportDataModel();

            SetData(data);
        }

        public void SetData(ReportDataModel item)
        {
            SystemKeyId = item.ReportId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.ReportLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Report;

            PlaceHolderCore = dynReportId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblReportId;
            CoreControlName = lblName;
            CoreControlDescription = lblDescription;           
            CoreControlSortOrder = lblSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblReportIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}