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

namespace ApplicationContainer.UI.Web.UseCasePackageXUseCase.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{
		#region properties

		public int? UseCasePackageXUseCaseId
		{
			get
			{
				if (txtUseCasePackageXUseCaseId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUseCasePackageXUseCaseId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtUseCasePackageXUseCaseId.Text);
				}
			}
		}

		public int? UseCasePackageId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtUseCasePackageId.Text.Trim());
				else
					return int.Parse(drpUseCasePackageList.SelectedItem.Value);
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
		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new UseCasePackageXUseCaseDataModel();

			data.UseCasePackageXUseCaseId	 = UseCasePackageXUseCaseId;
			data.UseCaseId					 = UseCaseId;
			data.UseCasePackageId			 = UseCasePackageId;			

			if (action == "Insert")
			{
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);
			}
			return UseCasePackageXUseCaseId;
		}

		public override void SetId(int setId, bool chkUseCasePackageXUseCaseId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkUseCasePackageXUseCaseId);
			txtUseCasePackageXUseCaseId.Enabled = chkUseCasePackageXUseCaseId;
			//txtUseCaseId.Enabled = !chkUseCasePackageXUseCaseId;			
			//txtUseCasePackageId.Enabled = !chkUseCasePackageXUseCaseId;

			//drpUseCaseRelationshipList.Enabled = !chkUseCasePackageXUseCaseId;
			//drpUseCaseList.Enabled = !chkUseCasePackageXUseCaseId;			
		}

		public void LoadData(int useCasePackageXUseCaseId, bool showId)
		{
			var data = new UseCasePackageXUseCaseDataModel();
			data.UseCasePackageXUseCaseId = useCasePackageXUseCaseId;
            var oUseCasePackageXUseCaseTable = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (oUseCasePackageXUseCaseTable.Rows.Count == 1)
			{
				var row = oUseCasePackageXUseCaseTable.Rows[0];


				if (!showId)
				{
					txtUseCasePackageXUseCaseId.Text = Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId]);

					dynAuditHistory.Visible = true;
				
					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.UseCasePackageXUseCase, useCasePackageXUseCaseId, "UseCasePackageXUseCase");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "UseCasePackageXUseCase");
				}
				else
				{
					txtUseCasePackageXUseCaseId.Text = String.Empty;
				}
				txtUseCasePackageId.Text	 = Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId]);
				txtUseCaseId.Text			 = Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId]);
								
				drpUseCaseList.SelectedValue		= Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId]);
				drpUseCasePackageList.SelectedValue = Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId]);
				oUpdateInfo.LoadText(oUseCasePackageXUseCaseTable.Rows[0]);
			}
			else
			{
				txtUseCasePackageXUseCaseId.Text = String.Empty;
				txtUseCaseId.Text = String.Empty;				
				txtUseCasePackageId.Text = String.Empty;

			}
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

            var useCasePackageXUseCaseData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(useCasePackageXUseCaseData, drpUseCasePackageList, StandardDataModel.StandardDataColumns.Name,
				UseCasePackageDataModel.DataColumns.UseCasePackageId);

            var useCaseData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(useCaseData, drpUseCaseList, StandardDataModel.StandardDataColumns.Name,
				UseCaseDataModel.DataColumns.UseCaseId);

            var useCasePackageData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(useCasePackageData, drpUseCasePackageList, StandardDataModel.StandardDataColumns.Name,
				UseCasePackageDataModel.DataColumns.UseCasePackageId);

			if (isTesting)
			{
				drpUseCaseList.AutoPostBack = true;
				drpUseCasePackageList.AutoPostBack = true;
				
				if (drpUseCasePackageList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtUseCasePackageId.Text.Trim()))
					{
						drpUseCasePackageList.SelectedValue = txtUseCasePackageId.Text;
					}
					else
					{
						txtUseCasePackageId.Text = drpUseCasePackageList.SelectedItem.Value;
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
				
				txtUseCasePackageId.Visible = true;
				txtUseCaseId.Visible = true;				
			}
			else
			{
				if (!string.IsNullOrEmpty(txtUseCasePackageId.Text.Trim()))
				{
					drpUseCasePackageList.SelectedValue = txtUseCasePackageId.Text;
				}
				if (!string.IsNullOrEmpty(txtUseCaseId.Text.Trim()))
				{
					drpUseCaseList.SelectedValue = txtUseCaseId.Text;
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
				txtUseCasePackageXUseCaseId.Visible = isTesting;
				lblUseCasePackageXUseCaseId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "UseCasePackageXUseCase";
			FolderLocationFromRoot = "/RequirementAnalysis";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCasePackageXUseCase;
          
			PlaceHolderCore = dynUseCasePackageXUseCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

		}

		protected void drpUseCasePackageList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCasePackageId.Text = drpUseCasePackageList.SelectedItem.Value;
		}

		protected void drpUseCaseList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseId.Text = drpUseCaseList.SelectedItem.Value;
		}				

		#endregion
	}
}