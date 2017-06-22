using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.DeliverableArtifact.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int deliverableArtifactId)
        {
            base.ShowData(deliverableArtifactId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new DeliverableArtifactDataModel();
            data.DeliverableArtifactId = deliverableArtifactId;

            var items = DeliverableArtifactDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];
                
                SetData(item);

                oHistoryList.Setup(PrimaryEntity, deliverableArtifactId, "DeliverableArtifact");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblDeliverableArtifactIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new DeliverableArtifactDataModel();

            SetData(data);
        }

        public void SetData(DeliverableArtifactDataModel item)
        {
            SystemKeyId = item.DeliverableArtifactId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.DeliverableArtifactLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DeliverableArtifact;

            PlaceHolderCore = dynDeliverableArtifactId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblDeliverableArtifactId;
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
                lblDeliverableArtifactIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}