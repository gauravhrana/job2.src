using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectXUseCase.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{
		#region properties		

		public int? ProjectXUseCaseId
		{
			get
			{
				if (txtProjectXUseCaseId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtProjectXUseCaseId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtProjectXUseCaseId.Text);
				}
			}
		}

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

		}

		public int? UseCaseId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtUseCaseId.Text.Trim());
				else
					return int.Parse(drpUseCaseList.SelectedItem.Value);
			}

		}

		public int? ProjectUseCaseStatusId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtProjectUseCaseStatusId.Text.Trim());
				else
					return int.Parse(drpProjectUseCaseStatusList.SelectedItem.Value);
			}

		}

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ProjectXUseCaseDataModel();

			data.ProjectXUseCaseId = ProjectXUseCaseId;
			data.UseCaseId = UseCaseId;
			data.ProjectId = ProjectId;
			data.ProjectUseCaseStatusId = ProjectUseCaseStatusId;

			if (action == "Insert")
			{
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of MilestoneID ?
			return ProjectXUseCaseId;
		}

		public override void SetId(int setId, bool chkProjectXUseCaseId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkProjectXUseCaseId);
			txtProjectXUseCaseId.Enabled = chkProjectXUseCaseId;
			//txtUseCaseId.Enabled = !chkProjectXUseCaseId;
			//txtProjectUseCaseStatusId.Enabled = !chkProjectXUseCaseId;
			//txtProjectId.Enabled = !chkProjectXUseCaseId;

			//drpProjectUseCaseStatusList.Enabled = !chkProjectXUseCaseId;
			//drpUseCaseList.Enabled = !chkProjectXUseCaseId;
			//drpProjectList.Enabled = !chkProjectXUseCaseId;
		}

		public void LoadData(int projectXUseCaseId, bool showId)
		{
			var data = new ProjectXUseCaseDataModel();
			data.ProjectXUseCaseId = projectXUseCaseId;
            var oProjectXUseCaseTable = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (oProjectXUseCaseTable.Rows.Count == 1)
			{
				var row = oProjectXUseCaseTable.Rows[0];


				if (!showId)
				{
					txtProjectXUseCaseId.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId]);

					dynAuditHistory.Visible = true;

					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ProjectXUseCase, projectXUseCaseId, "ProjectXUseCase");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ProjectXUseCase");
				}
				else
				{
					txtProjectXUseCaseId.Text = String.Empty;
				}
				txtProjectId.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.ProjectId]);
				txtUseCaseId.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.UseCaseId]);
				txtProjectUseCaseStatusId.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId]);

				drpProjectUseCaseStatusList.SelectedValue = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId]);
				drpUseCaseList.SelectedValue = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.UseCaseId]);
				drpProjectList.SelectedValue = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.ProjectId]);
				oUpdateInfo.LoadText(oProjectXUseCaseTable.Rows[0]);
			}
			else
			{
				txtProjectXUseCaseId.Text = String.Empty;
				txtUseCaseId.Text = String.Empty;
				txtProjectUseCaseStatusId.Text = String.Empty;
				txtProjectId.Text = String.Empty;

			}
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

            var projectXUseCaseData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(projectXUseCaseData, drpProjectList, ProjectXUseCaseDataModel.DataColumns.ProjectId,
				ProjectXUseCaseDataModel.DataColumns.ProjectId);


            var useCaseData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(useCaseData, drpUseCaseList, StandardDataModel.StandardDataColumns.Name,
				UseCaseDataModel.DataColumns.UseCaseId);

            var projectData = ProjectDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(projectData, drpProjectList, StandardDataModel.StandardDataColumns.Name,
				ProjectDataModel.DataColumns.ProjectId);


            var ProjectUseCaseStatusData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(ProjectUseCaseStatusData, drpProjectUseCaseStatusList, StandardDataModel.StandardDataColumns.Name, 
				ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId);

			if (isTesting)
			{
				drpUseCaseList.AutoPostBack = true;
				drpProjectList.AutoPostBack = true;
				drpProjectUseCaseStatusList.AutoPostBack = true;
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
				if (drpUseCaseList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtUseCaseId.Text.Trim()))
					{
						drpUseCaseList.SelectedValue = txtUseCaseId.Text;
					}
					else
					{
						txtUseCaseId.Text = drpUseCaseList.SelectedItem.Value;
					}
				}
				if (drpProjectUseCaseStatusList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtProjectUseCaseStatusId.Text.Trim()))
					{
						drpProjectUseCaseStatusList.SelectedValue = txtProjectUseCaseStatusId.Text;
					}
					else
					{
						txtProjectUseCaseStatusId.Text = drpProjectUseCaseStatusList.SelectedItem.Value;
					}
				}
				txtProjectId.Visible = true;
				txtUseCaseId.Visible = true;
				txtProjectUseCaseStatusId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtProjectId.Text.Trim()))
				{
					drpProjectList.SelectedValue = txtProjectId.Text;
				}
				if (!string.IsNullOrEmpty(txtUseCaseId.Text.Trim()))
				{
					drpUseCaseList.SelectedValue = txtUseCaseId.Text;
				}
				if (!string.IsNullOrEmpty(txtProjectUseCaseStatusId.Text.Trim()))
				{
					drpProjectUseCaseStatusList.SelectedValue = txtProjectUseCaseStatusId.Text;
				}
			}
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtProjectXUseCaseId.Visible = isTesting;
				lblProjectXUseCaseId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ProjectXUseCase";
			FolderLocationFromRoot = "/RequirementAnalysis";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectXUseCase;

			// set object variable reference            
			PlaceHolderCore = dynProjectXUseCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

		}

		protected void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtProjectId.Text = drpProjectList.SelectedItem.Value;
		}

		protected void drpUseCaseList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseId.Text = drpUseCaseList.SelectedItem.Value;
		}

		protected void drpProjectUseCaseStatusList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtProjectUseCaseStatusId.Text = drpProjectUseCaseStatusList.SelectedItem.Value;
		}

		#endregion
	}
}