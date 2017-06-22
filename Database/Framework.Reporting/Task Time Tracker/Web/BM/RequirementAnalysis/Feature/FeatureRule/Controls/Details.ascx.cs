using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.FeatureRule.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int featureRuleId)
		{
			base.ShowData(featureRuleId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new FeatureRuleDataModel();
			data.FeatureRuleId = featureRuleId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblFeatureRuleCategoryId.Text = item.FeatureRuleCategoryId.ToString();				
				SetData(item);

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, featureRuleId, "FeatureRule");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblFeatureRuleIdText, lblNameText, lblDescriptionText, lblSortOrderText,lblFeatureRuleCategoryIdText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new FeatureRuleDataModel();

			SetData(data);
		}

		public void SetData(FeatureRuleDataModel item)
		{
			SystemKeyId = item.FeatureRuleId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.FeatureRuleLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRule;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynFeatureRuleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblFeatureRuleId;
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
				lblFeatureRuleIdText.Visible = isTesting;
				lblFeatureRuleId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
	}
}