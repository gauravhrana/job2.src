using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.DeliverableArtifactStatus.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int deliverableArtifactStatusId)
        {
            base.ShowData(deliverableArtifactStatusId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new DeliverableArtifactStatusDataModel();
            data.DeliverableArtifactStatusId = deliverableArtifactStatusId;

            var items = DeliverableArtifactStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];               

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, deliverableArtifactStatusId, "DeliverableArtifactStatus");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblDeliverableArtifactStatusIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new DeliverableArtifactStatusDataModel();

            SetData(data);
        }

        public void SetData(DeliverableArtifactStatusDataModel item)
        {
            SystemKeyId = item.DeliverableArtifactStatusId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.DeliverableArtifactStatusLabelDictionary;
            PrimaryEntity = SystemEntity.DeliverableArtifactStatus;

            PlaceHolderCore = dynDeliverableArtifactStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblDeliverableArtifactStatusId;
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
                lblDeliverableArtifactStatusIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}