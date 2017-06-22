using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.FeatureRuleStatus.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int featureRuleStatusId)
        {
            base.ShowData(featureRuleStatusId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new FeatureRuleStatusDataModel();
            data.FeatureRuleStatusId = featureRuleStatusId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, featureRuleStatusId, "FeatureRuleStatus");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFeatureRuleStatusIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureRuleStatusDataModel();

            SetData(data);
        }

        public void SetData(FeatureRuleStatusDataModel item)
        {
            SystemKeyId = item.FeatureRuleStatusId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.FeatureRuleStatusLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleStatus;

            PlaceHolderCore = dynFeatureRuleStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblFeatureRuleStatusId;
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
                lblFeatureRuleStatusIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}