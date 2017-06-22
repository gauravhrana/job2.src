using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.FeatureXFeatureRule.Controls
{
    public partial class Details : ControlDetails
    {
        #region properties


        #endregion

        #region private methods

        protected override void ShowData(int FeatureXFeatureRuleId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new FeatureXFeatureRuleDataModel();
            data.FeatureXFeatureRuleId = FeatureXFeatureRuleId;

            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblFeatureXFeatureRuleId.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId]);
                lblFeatureRule.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureRule]);
                lblFeature.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.Feature]);
                lblFeatureRuleStatus.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatus]);

                oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule, FeatureXFeatureRuleId, "FeatureXFeatureRule");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "FeatureXFeatureRule");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblFeatureXFeatureRuleId.Text = String.Empty;
            lblFeature.Text = String.Empty;
            lblFeatureRule.Text = String.Empty;
            lblFeatureRuleStatus.Text = String.Empty;
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblFeatureXFeatureRuleId, lblFeature, lblFeatureRule, lblFeatureRuleStatus });
			}

			return LabelListCore;
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.FeatureXFeatureRuleLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule;

			base.OnInit(e);

			PlaceHolderCore = dynFeatureXFeatureRuleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblFeatureXFeatureRuleIdText.Visible = isTesting;
                lblFeatureXFeatureRuleId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

        

        #endregion
    }
}