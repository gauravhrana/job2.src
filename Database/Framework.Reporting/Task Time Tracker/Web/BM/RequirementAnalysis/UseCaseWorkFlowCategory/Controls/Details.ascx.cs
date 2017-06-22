using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.UseCaseWorkFlowCategory.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int UseCaseWorkFlowCategoryId)
        {
            base.ShowData(UseCaseWorkFlowCategoryId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new UseCaseWorkFlowCategoryDataModel();
            data.UseCaseWorkFlowCategoryId = UseCaseWorkFlowCategoryId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblCreatedByAuditId.Text = item.CreatedByAuditId.ToString();
                lblModifiedByAuditId.Text = item.ModifiedByAuditId.ToString();
                lblCreatedDate.Text = item.CreatedDate.Value.ToString(SessionVariables.UserDateFormat);
                lblModifiedDate.Text = item.ModifiedDate.Value.ToString(SessionVariables.UserDateFormat);

                SetData(item);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, UseCaseWorkFlowCategoryId, "UseCaseWorkFlowCategory");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUseCaseWorkFlowCategoryIdText, lblNameText, lblDescriptionText, lblSortOrderText,
                lblCreatedByAuditIdText, lblCreatedDateText, lblModifiedDateText,  lblModifiedByAuditIdText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCaseWorkFlowCategoryDataModel();

            SetData(data);
        }

        public void SetData(UseCaseWorkFlowCategoryDataModel item)
        {
            SystemKeyId = item.UseCaseWorkFlowCategoryId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.UseCaseWorkFlowCategoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseWorkFlowCategory;

            PlaceHolderCore = dynUseCaseWorkFlowCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblUseCaseWorkFlowCategoryId;
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
                lblUseCaseWorkFlowCategoryIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}