using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Entity.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{

		public int? WorkTicketId
		{
			get
			{
				return int.Parse(drpWorkTicketList.SelectedItem.Value);
			}
			set
			{
				txtWorkTicketId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

	

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;


			var WorkTicketData = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetEntityDetails(WorkTicketDataModel.Empty, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			UIHelper.LoadDropDown(WorkTicketData, drpWorkTicketList,
				StandardDataModel.StandardDataColumns.Name,
				WorkTicketDataModel.DataColumns.WorkTicketId);

		
			if (isTesting)
			{
				drpWorkTicketList.AutoPostBack = true;

				if (drpWorkTicketList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtEntityId.Text.Trim()))
					{
						drpWorkTicketList.SelectedValue = txtEntityId.Text;
					}
					else
					{
						txtWorkTicketId.Text = drpWorkTicketList.SelectedItem.Value;
					}
				}

				txtWorkTicketId.Visible = true;

			}
			else
			{
				if (!string.IsNullOrEmpty(txtWorkTicketId.Text.Trim()))
				{
					drpWorkTicketList.SelectedValue = txtWorkTicketId.Text;
				}

				txtWorkTicketId.Visible = false;

			}
		}



		#region methods

		public override int? Save(string action)
		{
			var data = new EntityDataModel();

			data.EntityId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;
			data.WorkTicketId = WorkTicketId;

			if (action == "Insert")
			{
				if(!TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.EntityId;
		}

		public override void SetId(int setId, bool chkEntityId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkEntityId);
			CoreSystemKey.Enabled = chkEntityId;
			//txtDescription.Enabled = !chkFunctionalityId;
			//txtName.Enabled = !chkFunctionalityId;
			//txtSortOrder.Enabled = !chkFunctionalityId;
		}

		public void LoadData(int entityId, bool showId)
		{
			Clear();

			var data = new EntityDataModel();
			data.EntityId = entityId;


			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count > 1)
			{
				var item1 = items[0];
				txtApplicationId.Text = item1.Application.ToString();
				txtName.Text = item1.Name;
				txtSortOrder.Text = item1.SortOrder.ToString();
				editor.Text = item1.Description;
				txtWorkTicketId.Text = item1.WorkTicketId.ToString(); ;
				
				//return;
			}


			var item = items[0];

			var applicationInfo = ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId];
			txtApplicationId.Text = applicationInfo.Description;

			txtApplicationId.Enabled = false;

		

			drpWorkTicketList.SelectedValue = item.WorkTicketId.ToString();
			txtWorkTicketId.Text = item.WorkTicketId.ToString();

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.EntityId;
				oHistoryList.Setup(PrimaryEntity, entityId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new EntityDataModel();

			SetData(data);
		}

		public void SetData(EntityDataModel data)
		{
			SystemKeyId = data.EntityId;
		

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				SetupDropdown();
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblEntityId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Entity;
			PrimaryEntityKey = "Entity";
			FolderLocationFromRoot = "Entity";

			PlaceHolderCore = dynEntityId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtEntityId;
			CoreControlName = txtName;
			CoreControlDescriptionKendoEditor = editor;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected void drpWorkTicketList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtWorkTicketId.Text = drpWorkTicketList.SelectedItem.Value;
		}

		

		#endregion

	}
}