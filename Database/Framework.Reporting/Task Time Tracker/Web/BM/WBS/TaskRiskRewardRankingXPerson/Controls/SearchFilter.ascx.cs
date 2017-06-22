using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RiskReward;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.ApplicationUser;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer.Task;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.WBS.TaskRiskRewardRankingXPerson.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

		public TaskRiskRewardRankingXPersonDataManager.Data SearchParameters
        {
            get
            {
				var data = new TaskRiskRewardRankingXPersonDataManager.Data();

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskRiskRewardRankingXPersonDataManager.DataColumns.TaskId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.TaskId) != "")
				{
					data.TaskId = Convert.ToInt32(
						CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.TaskId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskRiskRewardRankingXPersonDataManager.DataColumns.RiskId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.RiskId) != "")
				{
					data.RiskId = Convert.ToInt32(
						CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.RiskId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskRiskRewardRankingXPersonDataManager.DataColumns.RewardId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.RewardId) != "")
				{
					data.RewardId = Convert.ToInt32(
						CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.RewardId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskRiskRewardRankingXPersonDataManager.DataColumns.PersonId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.PersonId) != "")
				{
					data.PersonId = Convert.ToInt32(
						CheckAndGetFieldValue(TaskRiskRewardRankingXPersonDataManager.DataColumns.PersonId));
				}

                return data;
            }
        }

        #endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("RiskId"))
			{
                var riskData = RiskDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(riskData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					RiskDataModel.DataColumns.RiskId);
			}

			if (fieldName.Equals("RewardId"))
			{
                var rewardData = RewardDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(rewardData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					RewardDataModel.DataColumns.RewardId);
			}

			if (fieldName.Equals("TaskId"))
			{
                var taskData = TaskDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(taskData, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					TaskDataModel.DataColumns.TaskId);
			}

			if (fieldName.Equals("PersonId"))
			{
				var applicationUserdata = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationUserdata, dropDownListControl,
					ApplicationUserDataModel.DataColumns.FirstName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);
			}
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRiskRewardRankingXPerson;
			PrimaryEntityKey = "TaskRiskRewardRankingXPerson";
			FolderLocationFromRoot = "WBS/TaskRiskRewardRankingXPerson";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion        

    }
}