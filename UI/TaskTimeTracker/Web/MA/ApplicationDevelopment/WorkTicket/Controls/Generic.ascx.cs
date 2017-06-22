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

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{

		public int? EntityId
		{
			get
			{
				return int.Parse(drpEntityList.SelectedItem.Value);
			}
			set
			{
				txtWorkTicketId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}



		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;


			var EntityData = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.GetEntityDetails(EntityDataModel.Empty, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			UIHelper.LoadDropDown(EntityData, drpEntityList,
				StandardDataModel.StandardDataColumns.Name,
				EntityDataModel.DataColumns.EntityId);


			if (isTesting)
			{
				drpEntityList.AutoPostBack = true;

				if (drpEntityList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtEntityId.Text.Trim()))
					{
						drpEntityList.SelectedValue = txtEntityId.Text;
					}
					else
					{
						txtEntityId.Text = drpEntityList.SelectedItem.Value;
					}
				}

				txtEntityId.Visible = true;

			}
			else
			{
				if (!string.IsNullOrEmpty(txtEntityId.Text.Trim()))
				{
					drpEntityList.SelectedValue = txtEntityId.Text;
				}

				txtEntityId.Visible = false;

			}
		}



		#region methods

		public override int? Save(string action)
		{
			var data = new WorkTicketDataModel();

			data.WorkTicketId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;
			data.EntityId = EntityId;

			if (action == "Insert")
			{
				if(!TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.WorkTicketId;
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

			var data = new WorkTicketDataModel();
			data.WorkTicketId = entityId;


			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

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



			drpEntityList.SelectedValue = item.EntityId.ToString();
			txtEntityId.Text = item.EntityId.ToString();

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

			var data = new WorkTicketDataModel();

			SetData(data);
		}

		public void SetData(WorkTicketDataModel data)
		{
			SystemKeyId = data.WorkTicketId;


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
			lblWorkTicketId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.WorkTicket;
			PrimaryEntityKey = "WorkTicket";
			FolderLocationFromRoot = "WorkTicket";

			PlaceHolderCore = dynWorkTicketId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtWorkTicketId;
			CoreControlName = txtName;
			CoreControlDescriptionKendoEditor = editor;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected void drpEntityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtEntityId.Text = drpEntityList.SelectedItem.Value;
		}



		#endregion

	}
}