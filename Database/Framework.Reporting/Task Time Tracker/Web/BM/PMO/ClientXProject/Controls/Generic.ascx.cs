using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ClientXProject.Controls
{
	public partial class Generic : ControlGeneric
	{

		#region properties

		public int? ClientXProjectId
		{
			get
			{
				if (txtClientXProjectId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtClientXProjectId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtClientXProjectId.Text);
				}
			}
		}

		public int? ClientId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtClientId.Text.Trim());
				else
					return int.Parse(drpClientList.SelectedItem.Value);
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
		
		#endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new ClientXProjectDataModel();

			data.ClientXProjectId = ClientXProjectId;
			data.ClientId = ClientId;
			data.ProjectId = ProjectId;

			if (action == "Insert")
			{
                ClientXProjectDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				ClientXProjectDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.ClientXProjectId;
		}

		public override void SetId(int setId, bool chkClientXProjectId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkClientXProjectId);
			txtClientXProjectId.Enabled = chkClientXProjectId;			
		}

		public void LoadData(int ClientXProjectId, bool showId)
		{
			var data = new ClientXProjectDataModel();
			data.ClientXProjectId = ClientXProjectId;
            var oClientXProjectTable = ClientXProjectDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (oClientXProjectTable.Rows.Count == 1)
			{
				var row = oClientXProjectTable.Rows[0];

				if (!showId)
				{
					txtClientXProjectId.Text = Convert.ToString(row[ClientXProjectDataModel.DataColumns.ClientXProjectId]);

					dynAuditHistory.Visible = true;

					// only show Audit History in case of Update page, not for Clone.
					oHistoryList.Setup((int)SystemEntity.ClientXProject, ClientXProjectId, "ClientXProject");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ClientXProject");
				}
				else
				{
					txtClientXProjectId.Text = String.Empty;
                }
				txtClientId.Text = Convert.ToString(row[ClientXProjectDataModel.DataColumns.ClientId]);
				txtProjectId.Text = Convert.ToString(row[ClientXProjectDataModel.DataColumns.ProjectId]);

				drpProjectList.SelectedValue = Convert.ToString(row[ClientXProjectDataModel.DataColumns.ProjectId]);
				drpClientList.SelectedValue = Convert.ToString(row[ClientXProjectDataModel.DataColumns.ClientId]);

				oUpdateInfo.LoadText(oClientXProjectTable.Rows[0]);

			}
			else
			{
				txtClientXProjectId.Text = String.Empty;
				txtProjectId.Text = String.Empty;
				txtClientId.Text = String.Empty;
			}
		}

		

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var clientData = ClientDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(clientData, drpClientList, StandardDataModel.StandardDataColumns.Name, ClientDataModel.DataColumns.ClientId);

            var projectData = ProjectDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(projectData, drpProjectList, StandardDataModel.StandardDataColumns.Name, ProjectDataModel.DataColumns.ProjectId);

			if (isTesting)
			{
				drpProjectList.AutoPostBack = true;
				drpClientList.AutoPostBack = true;
				
				if (drpClientList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtClientId.Text.Trim()))
					{
						drpClientList.SelectedValue = txtClientId.Text;
					}
					else
					{
						txtClientId.Text = drpClientList.SelectedItem.Value;
					}
				}
				
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

				txtClientId.Visible = true;
				txtProjectId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtClientId.Text.Trim()))
				{
					drpClientList.SelectedValue = txtClientId.Text;
				}
				if (!string.IsNullOrEmpty(txtProjectId.Text.Trim()))
				{
					drpProjectList.SelectedValue = txtProjectId.Text;
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
				txtClientXProjectId.Visible = isTesting;
				lblClientXProjectId.Visible = isTesting;
				SetupDropdown();
			}
		}
		
		protected void drpClientList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtClientId.Text = drpClientList.SelectedItem.Value;
		}

		protected void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtProjectId.Text = drpProjectList.SelectedItem.Value;
		}

		protected override void OnInit(EventArgs e)
		{			
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.ClientXProject;
			PrimaryEntityKey = "ClientXProject";
			FolderLocationFromRoot = "ClientXProject";
			
			// set object variable reference            
			PlaceHolderCore			= dynClientId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv				= borderdiv;
			
			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

		}

		#endregion

	}
}