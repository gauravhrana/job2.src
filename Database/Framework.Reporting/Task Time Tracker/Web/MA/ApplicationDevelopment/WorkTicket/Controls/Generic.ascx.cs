using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{

		#region methods

		public override int? Save(string action)
		{
			var data = new WorkTicketDataModel();

			data.WorkTicketId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				var dtWorkTicket = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtWorkTicket.Rows.Count == 0)
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

		public override void SetId(int setId, bool chkWorkTicketId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkWorkTicketId);
			CoreSystemKey.Enabled = chkWorkTicketId;
			//txtDescription.Enabled = !chkWorkTicketId;
			//txtName.Enabled = !chkWorkTicketId;
			//txtSortOrder.Enabled = !chkWorkTicketId;
		}

		public void LoadData(int functionalityActiveStatusId, bool showId)
		{
			Clear();

			var data = new WorkTicketDataModel();
			data.WorkTicketId = functionalityActiveStatusId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.WorkTicketId;
				oHistoryList.Setup(PrimaryEntity, functionalityActiveStatusId, PrimaryEntityKey);
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

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtWorkTicketId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion

	}
}