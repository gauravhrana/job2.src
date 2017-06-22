using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCaseActorXUseCase.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{
		#region properties		

		public int? UseCaseActorXUseCaseId
		{
			get
			{
				if (txtUseCaseActorXUseCaseId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUseCaseActorXUseCaseId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtUseCaseActorXUseCaseId.Text);
				}
			}
		}

		public int? UseCaseActorId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtUseCaseActorId.Text.Trim());
				else
					return int.Parse(drpUseCaseActorList.SelectedItem.Value);
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

		public int? UseCaseRelationshipId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtUseCaseRelationshipId.Text.Trim());
				else
					return int.Parse(drpUseCaseRelationshipList.SelectedItem.Value);
			}

		}		

		#endregion properties

        #region private methods

        public override int? Save(string action)
        {
            var data = new UseCaseActorXUseCaseDataModel();

            data.UseCaseActorXUseCaseId      = UseCaseActorXUseCaseId;
            data.UseCaseId                   = UseCaseId;
            data.UseCaseActorId              = UseCaseActorId;
            data.UseCaseRelationshipId       = UseCaseRelationshipId;

            if (action == "Insert")
            {
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Create(data, SessionVariables.RequestProfile);
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of MilestoneID ?
            return UseCaseActorXUseCaseId;
        }

		public override void SetId(int setId, bool chkUseCaseActorXUseCaseId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkUseCaseActorXUseCaseId);
			txtUseCaseActorXUseCaseId.Enabled = chkUseCaseActorXUseCaseId;
			//txtUseCaseId.Enabled = !chkUseCaseActorXUseCaseId;
			//txtUseCaseRelationshipId.Enabled = !chkUseCaseActorXUseCaseId;
			//txtUseCaseActorId.Enabled = !chkUseCaseActorXUseCaseId;

			//drpUseCaseRelationshipList.Enabled = !chkUseCaseActorXUseCaseId;
			//drpUseCaseList.Enabled = !chkUseCaseActorXUseCaseId;
			//drpUseCaseActorList.Enabled = !chkUseCaseActorXUseCaseId;
		}

        public void LoadData(int useCaseActorXUseCaseId, bool showId)
        {
            var data = new UseCaseActorXUseCaseDataModel();
            data.UseCaseActorXUseCaseId = useCaseActorXUseCaseId;
            var oUseCaseActorXUseCaseTable = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oUseCaseActorXUseCaseTable.Rows.Count == 1)
            {
                var row = oUseCaseActorXUseCaseTable.Rows[0];


				if (!showId)
				{
                    txtUseCaseActorXUseCaseId.Text = Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId]);

					dynAuditHistory.Visible = true;

					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase, useCaseActorXUseCaseId, "UseCaseActorXUseCase");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "UseCaseActorXUseCase");					
				}
				else
				{
					txtUseCaseActorXUseCaseId.Text = String.Empty;
				}
				txtUseCaseActorId.Text			= Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId]);
				txtUseCaseId.Text				= Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId]);
				txtUseCaseRelationshipId.Text	= Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId]);

				drpUseCaseRelationshipList.SelectedValue	= Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId]);
				drpUseCaseList.SelectedValue				= Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId]);
				drpUseCaseActorList.SelectedValue			= Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId]);
				oUpdateInfo.LoadText(oUseCaseActorXUseCaseTable.Rows[0]);
			}
			else
			{
				txtUseCaseActorXUseCaseId.Text = String.Empty;
				txtUseCaseId.Text = String.Empty;
				txtUseCaseRelationshipId.Text = String.Empty;
				txtUseCaseActorId.Text = String.Empty;

			}
		}		

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

            var useCaseActorXUseCaseData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(useCaseActorXUseCaseData, drpUseCaseActorList, StandardDataModel.StandardDataColumns.Name,
				UseCaseActorDataModel.DataColumns.UseCaseActorId);

            var taskData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskData, drpUseCaseList, StandardDataModel.StandardDataColumns.Name,
				UseCaseDataModel.DataColumns.UseCaseId);

			//var UseCaseRelationshipData = Framework.Components.ApplicationUser.ApplicationUser.GetList(SessionVariables.RequestProfile.AuditId);
			//UIHelper.LoadDropDown(UseCaseRelationshipData, drpUseCaseRelationshipList, ApplicationUserDataModel.DataColumns.FirstName, ApplicationUserDataModel.DataColumns.ApplicationUserId);
            var UseCaseRelationshipData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(UseCaseRelationshipData, drpUseCaseRelationshipList, StandardDataModel.StandardDataColumns.Name, UseCaseRelationshipDataModel.DataColumns.UseCaseRelationshipId);

			if (isTesting)
			{
				drpUseCaseList.AutoPostBack = true;
				drpUseCaseActorList.AutoPostBack = true;
				drpUseCaseRelationshipList.AutoPostBack = true;
				if (drpUseCaseActorList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtUseCaseActorId.Text.Trim()))
					{
						drpUseCaseActorList.SelectedValue = txtUseCaseActorId.Text;
					}
					else
					{
						txtUseCaseActorId.Text = drpUseCaseActorList.SelectedItem.Value;
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
				if (drpUseCaseRelationshipList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtUseCaseRelationshipId.Text.Trim()))
					{
						drpUseCaseRelationshipList.SelectedValue = txtUseCaseRelationshipId.Text;
					}
					else
					{
						txtUseCaseRelationshipId.Text = drpUseCaseRelationshipList.SelectedItem.Value;
					}
				}
				txtUseCaseActorId.Visible = true;
				txtUseCaseId.Visible = true;
				txtUseCaseRelationshipId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtUseCaseActorId.Text.Trim()))
				{
					drpUseCaseActorList.SelectedValue = txtUseCaseActorId.Text;
				}
				if (!string.IsNullOrEmpty(txtUseCaseId.Text.Trim()))
				{
					drpUseCaseList.SelectedValue = txtUseCaseId.Text;
				}
				if (!string.IsNullOrEmpty(txtUseCaseRelationshipId.Text.Trim()))
				{
					drpUseCaseRelationshipList.SelectedValue = txtUseCaseRelationshipId.Text;
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
				txtUseCaseActorXUseCaseId.Visible = isTesting;
				lblUseCaseActorXUseCaseId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "UseCaseActorXUseCase";
			FolderLocationFromRoot = "/RequirementAnalysis";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase;

			// set object variable reference            
			PlaceHolderCore = dynUseCaseActorXUseCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
			
		}        

		protected void drpUseCaseActorList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseActorId.Text = drpUseCaseActorList.SelectedItem.Value;
		}

		protected void drpUseCaseList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseId.Text = drpUseCaseList.SelectedItem.Value;
		}

		protected void drpUseCaseRelationshipList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseRelationshipId.Text = drpUseCaseRelationshipList.SelectedItem.Value;
		}

		#endregion
	}
}