using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Milestone.Controls
{
	public partial class Generic : ControlGenericStandard
	{

        #region properties				

		public int? ProjectId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;

				if (isTesting)
					return int.Parse(txtProjectId.Text.Trim());
				else
					return int.Parse(drpProjectList.SelectedItem.Value);
			}
			set
			{
				txtProjectId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}		

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new MilestoneDataModel();

			data.MilestoneId = SystemKeyId;
			data.ProjectId = ProjectId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
                var dtMilestone = MilestoneDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtMilestone.Rows.Count == 0)
				{
                    MilestoneDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                MilestoneDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of MilestoneID ?
			return data.MilestoneId;
		}

		public override void SetId(int setId, bool chkMilestoneId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkMilestoneId);
			txtMilestoneId.Enabled = chkMilestoneId;
			//txtDescription.Enabled = !chkMilestoneId;
			//txtName.Enabled = !chkMilestoneId;
			//txtSortOrder.Enabled = !chkMilestoneId;
			//drpProjectList.Enabled = !chkMilestoneId;
			//txtProjectId.Enabled = !chkMilestoneId;
		}

		public void LoadData(int milestoneId, bool showId)
		{
			Clear();

			var data = new MilestoneDataModel();

			//var projectData = TaskTimeTracker.Components.BusinessLayer.Project.GetList(SessionVariables.RequestProfile.AuditId);
			//var data = new MilestoneDataModel();
			
			data.MilestoneId = milestoneId;
            var items = MilestoneDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.MilestoneId;
				ProjectId = item.ProjectId;

				oHistoryList.Setup(PrimaryEntity, milestoneId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}

			/*if (items.Rows.Count == 1)
			{
				var row = oMilestoneTable.Rows[0];

				if (!showId)
				{
					txtMilestoneId.Text = Convert.ToString(row[TaskTimeTracker.MilestoneDataModel.DataColumns.MilestoneId]);

					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.Milestone, milestoneId, "Milestone");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "Milestone");

				}

				else
				{
					txtMilestoneId.Text = String.Empty;
				}
				txtDescription.InnerText        = Convert.ToString(row[TaskTimeTracker.MilestoneDataModel.DataColumns.Description]);
				txtName.Text                    = Convert.ToString(row[TaskTimeTracker.MilestoneDataModel.DataColumns.Name]);
				txtSortOrder.Text               = Convert.ToString(row[TaskTimeTracker.MilestoneDataModel.DataColumns.SortOrder]);
				drpProjectList.SelectedValue    = Convert.ToString(row[TaskTimeTracker.MilestoneDataModel.DataColumns.ProjectId]);
				txtProjectId.Text               = Convert.ToString(row[TaskTimeTracker.MilestoneDataModel.DataColumns.ProjectId]);

				oUpdateInfo.LoadText(oMilestoneTable.Rows[0]);
			}
			else
			{
				txtMilestoneId.Text = String.Empty;
				txtDescription.InnerText = String.Empty;
				txtName.Text = String.Empty;
				txtSortOrder.Text = String.Empty;
			}*/
		}		

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
            var projectData = ProjectDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(projectData, drpProjectList, StandardDataModel.StandardDataColumns.Name, ProjectDataModel.DataColumns.ProjectId);

			if (isTesting)
			{
				drpProjectList.AutoPostBack = true;

				if (drpProjectList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtProjectId.Text.Trim()))
					{
						drpProjectList.SelectedValue = txtProjectId.Text;
					}
					else
					{
						txtProjectId.Text = drpProjectList.SelectedItem.Value;
					}
				}

				txtProjectId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtProjectId.Text.Trim()))
				{
					drpProjectList.SelectedValue = txtProjectId.Text;
				}
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new MilestoneDataModel();

			SetData(data);
		}

		public void SetData(MilestoneDataModel data)
		{
			SystemKeyId = data.MilestoneId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtMilestoneId.Visible = isTesting;
				lblMilestoneId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.Milestone;
			PrimaryEntityKey = "Milestone";
			FolderLocationFromRoot = "Milestone";

			// set object variable reference            
			PlaceHolderCore = dynMilestoneId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtMilestoneId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;	
		}

		protected void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtProjectId.Text = drpProjectList.SelectedItem.Value;
		}

		#endregion

	}
}