using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.RiskReward;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Demo.Controls
{
	public partial class Details : ControlDetailsStandard
	{

		protected override void ShowData(int RiskId) 
		{
			base.ShowData(RiskId);
			oDetailButtonPanel.SetId = SetId;
			Clear();

			var data = new RiskDataModel();
			data.RiskId = RiskId;

			var items = RiskDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			if (items.Count == 1)
			{
				var item = items[0];
				SetData(item);
				oHistoryList.Setup(PrimaryEntity, RiskId, "Risk");
			}
		}

		public void SetData(RiskDataModel data)
		{
			SystemKeyId = data.RiskId;
			base.SetData(data);
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new[] { lblRiskIdText,
				lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();
			var data = new RiskDataModel();
			SetData(data);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.RiskLabelDictionary;
			PrimaryEntity = SystemEntity.Risk;

			PlaceHolderCore			= dynRiskId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv				= borderdiv;

			CoreSystemKey			= lblRiskId;
			CoreControlName			= lblName;
			CoreControlDescription	= lblDescription;
			CoreControlSortOrder	= lblSortOrder;

			CoreUpdateInfo			= oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblRiskIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}

			PopulateLabelsText();
		}

	}

}
