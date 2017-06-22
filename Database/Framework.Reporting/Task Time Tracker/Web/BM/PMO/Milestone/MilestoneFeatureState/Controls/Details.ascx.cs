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

namespace ApplicationContainer.UI.Web.MilestoneFeatureState.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int milestoneFeatureStateId)
        {
            base.ShowData(milestoneFeatureStateId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new MilestoneFeatureStateDataModel();
            data.MilestoneFeatureStateId = milestoneFeatureStateId;

            var items = MilestoneFeatureStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, milestoneFeatureStateId, "MilestoneFeatureState");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblMilestoneFeatureStateIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new MilestoneFeatureStateDataModel();

            SetData(data);
        }

        public void SetData(MilestoneFeatureStateDataModel item)
        {
            SystemKeyId = item.MilestoneFeatureStateId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.MilestoneFeatureStateLabelDictionary;
            PrimaryEntity = SystemEntity.MilestoneFeatureState;

            PlaceHolderCore = dynMilestoneFeatureStateId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblMilestoneFeatureStateId;
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
                lblMilestoneFeatureStateIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}