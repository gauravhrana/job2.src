using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.WBS.TaskRiskRewardRankingXPerson.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
		#region private methods

		protected override void ShowData(int taskRiskRewardRankingXPersonId)
		{
			base.ShowData(taskRiskRewardRankingXPersonId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TaskRiskRewardRankingXPersonDataManager.Data();
			data.TaskRiskRewardRankingXPersonId = taskRiskRewardRankingXPersonId;

            var items = TaskRiskRewardRankingXPersonDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblTaskRiskRewardRankingXPersonId.Text = item.TaskRiskRewardRankingXPersonId.ToString();
				lblTask.Text = item.TaskId.ToString();
				lblRisk.Text = item.RiskId.ToString();
				lblReward.Text = item.RewardId.ToString();
				lblPerson.Text = item.PersonId.ToString();
				lblRanking.Text = item.Ranking.ToString();		
				
				oHistoryList.Setup(PrimaryEntity, taskRiskRewardRankingXPersonId, "TaskRiskRewardRankingXPerson");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskRiskRewardRankingXPersonIdText, lblTaskText, lblRiskText, lblRewardText, lblRankingText
													  , lblPersonText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.TaskRiskRewardRankingXPersonLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRiskRewardRankingXPerson;

			base.OnInit(e);

			PlaceHolderCore = dynTaskRiskRewardRankingXPersonId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTaskRiskRewardRankingXPersonIdText.Visible = isTesting;
				lblTaskRiskRewardRankingXPersonId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
        
	}

}