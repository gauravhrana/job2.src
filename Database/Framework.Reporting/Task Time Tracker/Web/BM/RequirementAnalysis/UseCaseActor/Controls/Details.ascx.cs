using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.UseCaseActor.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int useCaseActorId)
        {
            base.ShowData(useCaseActorId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new UseCaseActorDataModel();
            data.UseCaseActorId = useCaseActorId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblCreatedByAuditId.Text = item.CreatedByAuditId.ToString();
                lblModifiedByAuditId.Text = item.ModifiedByAuditId.ToString();
                lblCreatedDate.Text = item.CreatedDate.Value.ToString(SessionVariables.UserDateFormat);
                lblModifiedDate.Text = item.ModifiedDate.Value.ToString(SessionVariables.UserDateFormat);

                SetData(item);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, useCaseActorId, "UseCaseActor");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUseCaseActorIdText, lblNameText, lblDescriptionText, lblSortOrderText,
                lblCreatedByAuditIdText, lblCreatedDateText, lblModifiedDateText,  lblModifiedByAuditIdText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCaseActorDataModel();

            SetData(data);
        }

        public void SetData(UseCaseActorDataModel item)
        {
            SystemKeyId = item.UseCaseActorId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.UseCaseActorLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActor;

            PlaceHolderCore = dynUseCaseActorId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblUseCaseActorId;
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
                lblUseCaseActorIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}