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

namespace ApplicationContainer.UI.Web.UseCaseXUseCaseStep.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{
		#region properties
		
		public int? UseCaseXUseCaseStepId
		{
			get
			{
				if (txtUseCaseXUseCaseStepId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUseCaseXUseCaseStepId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtUseCaseXUseCaseStepId.Text);
				}
			}
			set
			{
				txtUseCaseXUseCaseStepId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? UseCaseStepId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtUseCaseStepId.Text.Trim());
				else
					return int.Parse(drpUseCaseStepList.SelectedItem.Value);
			}
			set
			{
				txtUseCaseStepId.Text = (value == null) ? String.Empty : value.ToString();
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
			set
			{
				txtUseCaseId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? UseCaseWorkFlowCategoryId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtUseCaseWorkFlowCategoryId.Text.Trim());
				else
					return int.Parse(drpUseCaseWorkFlowCategoryList.SelectedItem.Value);
			}
			set
			{
				txtUseCaseWorkFlowCategoryId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new UseCaseXUseCaseStepDataModel();

			data.UseCaseXUseCaseStepId		= UseCaseXUseCaseStepId;
			data.UseCaseId					= UseCaseId;
			data.UseCaseStepId				= UseCaseStepId;
			data.UseCaseWorkFlowCategoryId	= UseCaseWorkFlowCategoryId;

			if (action == "Insert")
			{
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Update(data, SessionVariables.RequestProfile);
			}
		
			return UseCaseXUseCaseStepId;
		}

		public override void SetId(int setId, bool chkUseCaseXUseCaseStepId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkUseCaseXUseCaseStepId);
			txtUseCaseXUseCaseStepId.Enabled = chkUseCaseXUseCaseStepId;
			//txtUseCaseId.Enabled = !chkUseCaseXUseCaseStepId;
			//txtUseCaseWorkFlowCategoryId.Enabled = !chkUseCaseXUseCaseStepId;
			//txtUseCaseStepId.Enabled = !chkUseCaseXUseCaseStepId;

			//drpUseCaseWorkFlowCategoryList.Enabled = !chkUseCaseXUseCaseStepId;
			//drpUseCaseList.Enabled = !chkUseCaseXUseCaseStepId;
			//drpUseCaseStepList.Enabled = !chkUseCaseXUseCaseStepId;
		}

        public void LoadData(int useCaseXUseCaseStepId, bool showId)
		{
			//Clear();

			var dataQuery = new UseCaseXUseCaseStepDataModel();
			dataQuery.UseCaseXUseCaseStepId = useCaseXUseCaseStepId;

			var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			UseCaseXUseCaseStepId		= item.UseCaseXUseCaseStepId;
			UseCaseId					= item.UseCaseId;
			UseCaseStepId				= item.UseCaseStepId;
			UseCaseWorkFlowCategoryId	= item.UseCaseWorkFlowCategoryId;

			if (!showId)
			{
				txtUseCaseXUseCaseStepId.Text = item.UseCaseXUseCaseStepId.ToString();
				oHistoryList.Setup(PrimaryEntity, useCaseXUseCaseStepId, PrimaryEntityKey);
			}
			else
			{
				txtUseCaseXUseCaseStepId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new UseCaseXUseCaseStepDataModel();

			UseCaseXUseCaseStepId		= data.UseCaseXUseCaseStepId;
			UseCaseId					= data.UseCaseId;
			UseCaseStepId				= data.UseCaseStepId;
			UseCaseWorkFlowCategoryId	= data.UseCaseWorkFlowCategoryId;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			//var useCasexUseCaseStepData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
			//UIHelper.LoadDropDown(useCasexUseCaseStepData, drpUseCaseStepList, StandardDataModel.StandardDataColumns.Name,
			//	UseCaseStepDataModel.DataColumns.UseCaseStepId);

            var useCaseData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(useCaseData, drpUseCaseList, StandardDataModel.StandardDataColumns.Name,
				UseCaseDataModel.DataColumns.UseCaseId);

            var useCaseStepData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(useCaseStepData, drpUseCaseStepList, StandardDataModel.StandardDataColumns.Name,
				UseCaseStepDataModel.DataColumns.UseCaseStepId);

            var UseCaseWorkFlowCategoryData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(UseCaseWorkFlowCategoryData, drpUseCaseWorkFlowCategoryList, StandardDataModel.StandardDataColumns.Name, UseCaseWorkFlowCategoryDataModel.DataColumns.UseCaseWorkFlowCategoryId);

			if (isTesting)
			{
				drpUseCaseList.AutoPostBack = true;
				drpUseCaseStepList.AutoPostBack = true;
				drpUseCaseWorkFlowCategoryList.AutoPostBack = true;
				if (drpUseCaseStepList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtUseCaseStepId.Text.Trim()))
					{
						drpUseCaseStepList.SelectedValue = txtUseCaseStepId.Text;
					}
					else
					{
						txtUseCaseStepId.Text = drpUseCaseStepList.SelectedItem.Value;
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
				if (drpUseCaseWorkFlowCategoryList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtUseCaseWorkFlowCategoryId.Text.Trim()))
					{
						drpUseCaseWorkFlowCategoryList.SelectedValue = txtUseCaseWorkFlowCategoryId.Text;
					}
					else
					{
						txtUseCaseWorkFlowCategoryId.Text = drpUseCaseWorkFlowCategoryList.SelectedItem.Value;
					}
				}
				txtUseCaseStepId.Visible = true;
				txtUseCaseId.Visible = true;
				txtUseCaseWorkFlowCategoryId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtUseCaseStepId.Text.Trim()))
				{
					drpUseCaseStepList.SelectedValue = txtUseCaseStepId.Text;
				}
				if (!string.IsNullOrEmpty(txtUseCaseId.Text.Trim()))
				{
					drpUseCaseList.SelectedValue = txtUseCaseId.Text;
				}
				if (!string.IsNullOrEmpty(txtUseCaseWorkFlowCategoryId.Text.Trim()))
				{
					drpUseCaseWorkFlowCategoryList.SelectedValue = txtUseCaseWorkFlowCategoryId.Text;
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
				txtUseCaseXUseCaseStepId.Visible = isTesting;
				lblUseCaseXUseCaseStepId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep;
			PrimaryEntityKey = "UseCaseXUseCaseStep";
			FolderLocationFromRoot = "/RequirementAnalysis";
			

			// set object variable reference            
			PlaceHolderCore = dynUseCaseXUseCaseStepId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

		}

		protected void drpUseCaseStepList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseStepId.Text = drpUseCaseStepList.SelectedItem.Value;
		}

		protected void drpUseCaseList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseId.Text = drpUseCaseList.SelectedItem.Value;
		}

		protected void drpUseCaseWorkFlowCategoryList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtUseCaseWorkFlowCategoryId.Text = drpUseCaseWorkFlowCategoryList.SelectedItem.Value;
		}

		#endregion
	}
}