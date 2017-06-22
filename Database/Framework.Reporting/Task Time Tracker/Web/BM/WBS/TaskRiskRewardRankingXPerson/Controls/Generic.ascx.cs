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
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.WBS.TaskRiskRewardRankingXPerson.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? TaskRiskRewardRankingXPersonId
        {
            get
            {
                if (txtTaskRiskRewardRankingXPersonId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskRiskRewardRankingXPersonId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTaskRiskRewardRankingXPersonId.Text);
                }
            }
			set
			{
				txtTaskRiskRewardRankingXPersonId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? RiskId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtRiskId.Text.Trim());
                else
                    return int.Parse(drpRiskList.SelectedItem.Value);
            }

			set
			{
				txtRiskId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public int? Ranking
        {
            get
            {
                return int.Parse(txtRanking.Text.Trim());
            }
			set
			{
				txtRanking.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? RewardId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtRewardId.Text.Trim());
                else
                    return int.Parse(drpRewardList.SelectedItem.Value);
            }
			set
			{
				txtRewardId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public int? TaskId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTaskId.Text.Trim());
                else
                    return int.Parse(drpTaskList.SelectedItem.Value);
            }
			set
			{
				txtTaskId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public int? PersonId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtPersonId.Text.Trim());
                else
                    return int.Parse(drpPersonList.SelectedItem.Value);
            }
			set
			{
				txtPersonId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }
        

        #endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new TaskRiskRewardRankingXPersonDataManager.Data();

			data.TaskRiskRewardRankingXPersonId = TaskRiskRewardRankingXPersonId;
			data.TaskId = TaskId;
			data.RewardId = RewardId;
			data.Ranking = Ranking;
			data.PersonId = PersonId;

			if (action == "Insert")
			{
                var dtTaskRiskRewardRankingXPerson = TaskRiskRewardRankingXPersonDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTaskRiskRewardRankingXPerson.Rows.Count == 0)
				{
                    TaskRiskRewardRankingXPersonDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskRiskRewardRankingXPersonDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return TaskRiskRewardRankingXPersonId;
		}

		public override void SetId(int setId, bool chkTaskRiskRewardRankingXPersonId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskRiskRewardRankingXPersonId);
			txtTaskRiskRewardRankingXPersonId.Enabled = chkTaskRiskRewardRankingXPersonId;
			//txtDescription.Enabled = !chkTaskRiskRewardRankingXPersonId;
			//txtActivityId.Enabled = !chkTaskRiskRewardRankingXPersonId;
			//txtSortOrder.Enabled = !chkTaskRiskRewardRankingXPersonId;
		}

		public void LoadData(int taskRiskRewardRankingXPersonId, bool showId)
		{

			Clear();

			var data = new TaskRiskRewardRankingXPersonDataManager.Data();
			data.TaskRiskRewardRankingXPersonId = taskRiskRewardRankingXPersonId;
            var items = TaskRiskRewardRankingXPersonDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


			if (items.Count != 1) return;

			var item = items[0];

			TaskRiskRewardRankingXPersonId = item.TaskRiskRewardRankingXPersonId;
			TaskId = item.TaskId;
			RiskId = item.RiskId;
			RewardId = item.RewardId;
			PersonId = item.PersonId;

			if (!showId)
			{
				txtTaskRiskRewardRankingXPersonId.Text = item.TaskRiskRewardRankingXPersonId.ToString();
				oHistoryList.Setup(PrimaryEntity, taskRiskRewardRankingXPersonId, PrimaryEntityKey);
			}
			else
			{
				txtTaskRiskRewardRankingXPersonId.Text = String.Empty;
			}
			
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskRiskRewardRankingXPersonDataManager.Data();

			TaskRiskRewardRankingXPersonId = data.TaskRiskRewardRankingXPersonId;
			TaskId = data.TaskId;
			RiskId = data.RiskId;
			RewardId = data.RewardId;
			PersonId = data.PersonId;
		}		

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtTaskRiskRewardRankingXPersonId.Visible = isTesting;
				lblTaskRiskRewardRankingXPersonId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRiskRewardRankingXPerson;
			PrimaryEntityKey = "TaskRiskRewardRankingXPerson";
			FolderLocationFromRoot = "TaskRiskRewardRankingXPerson";

			PlaceHolderCore = dynTaskRiskRewardRankingXPersonId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpRiskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtRiskId.Text = drpRiskList.SelectedItem.Value;
		}

		protected void drpRewardList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtRewardId.Text = drpRewardList.SelectedItem.Value;
		}

		protected void drpTaskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskId.Text = drpTaskList.SelectedItem.Value;
		}

		protected void drpPersonList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtPersonId.Text = drpPersonList.SelectedItem.Value;
		}

		#endregion

        #region private methods
       
        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var rewardData = RewardDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(rewardData, drpRewardList, StandardDataModel.StandardDataColumns.Name,
                RewardDataModel.DataColumns.RewardId);


            var riskData = RiskDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(riskData, drpRiskList, StandardDataModel.StandardDataColumns.Name,
				RiskDataModel.DataColumns.RiskId);


            var taskData = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskData, drpTaskList, StandardDataModel.StandardDataColumns.Name,
                TaskDataModel.DataColumns.TaskId);


			var personData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(personData, drpPersonList, ApplicationUserDataModel.DataColumns.FirstName, ApplicationUserDataModel.DataColumns.ApplicationUserId);

            if (isTesting)
            {
                drpTaskList.AutoPostBack = true;
                drpRewardList.AutoPostBack = true;
                drpPersonList.AutoPostBack = true;
                drpRiskList.AutoPostBack = true;
                if (drpRiskList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtRiskId.Text.Trim()))
                    {
                        drpRiskList.SelectedValue = txtRiskId.Text;
                    }
                    else
                    {
                        txtRiskId.Text = drpRiskList.SelectedItem.Value;
                    }
                }
                if (drpRewardList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtRewardId.Text.Trim()))
                    {
                        drpRewardList.SelectedValue = txtRewardId.Text;
                    }
                    else
                    {
                        txtRewardId.Text = drpRewardList.SelectedItem.Value;
                    }
                }
                if (drpTaskList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
                    {
                        drpTaskList.SelectedValue = txtTaskId.Text;
                    }
                    else
                    {
                        txtTaskId.Text = drpTaskList.SelectedItem.Value;
                    }
                }
                if (drpPersonList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
                    {
                        drpPersonList.SelectedValue = txtPersonId.Text;
                    }
                    else
                    {
                        txtPersonId.Text = drpPersonList.SelectedItem.Value;
                    }
                }
                txtRiskId.Visible = true;
                txtRewardId.Visible = true;
                txtTaskId.Visible = true;
                txtPersonId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtRiskId.Text.Trim()))
                {
                    drpRiskList.SelectedValue = txtRiskId.Text;
                }
                if (!string.IsNullOrEmpty(txtRewardId.Text.Trim()))
                {
                    drpRewardList.SelectedValue = txtRewardId.Text;
                }
                if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
                {
                    drpTaskList.SelectedValue = txtTaskId.Text;
                }
                if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
                {
                    drpPersonList.SelectedValue = txtPersonId.Text;
                }
            }
        }

        #endregion        

    }
}