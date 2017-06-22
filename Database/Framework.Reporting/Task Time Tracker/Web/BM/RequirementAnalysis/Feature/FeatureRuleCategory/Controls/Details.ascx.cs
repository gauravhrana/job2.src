using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.FeatureRuleCategory.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int featureRuleCategoryId)
        {
            base.ShowData(featureRuleCategoryId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new FeatureRuleCategoryDataModel();
            data.FeatureRuleCategoryId = featureRuleCategoryId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, featureRuleCategoryId, "FeatureRuleCategory");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFeatureRuleCategoryIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureRuleCategoryDataModel();

            SetData(data);
        }

        public void SetData(FeatureRuleCategoryDataModel item)
        {
            SystemKeyId = item.FeatureRuleCategoryId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.FeatureRuleCategoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleCategory;

            PlaceHolderCore = dynFeatureRuleCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblFeatureRuleCategoryId;
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
                lblFeatureRuleCategoryIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}