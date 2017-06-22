using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

		public int? EntityXWorkTicketId
		{
			get
			{
				if (txtEntityXWorkTicketId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtEntityXWorkTicketId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtEntityXWorkTicketId.Text);
				}
			}
		}

		public int EntityId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtEntityId.Text.Trim());
				else
					return int.Parse(drpEntityList.SelectedItem.Value);
			}
		}

		public int WorkTicketId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtWorkTicketId.Text.Trim());
				else
					return int.Parse(drpWorkTicketList.SelectedItem.Value);
			}
		}

		public string AcknowledgedBy
		{
			get
			{
				return txtAcknowledgedBy.Text;
			}
		}

		public string Memo
		{
			get
			{
				return txtMemo.Text;
			}
		}

		public int KnowledgeDate
		{
			get
			{
				return int.Parse(DateTimeHelper.FromUserDateFormatToDate(txtKnowledgeDate.Text.Trim()).Value.ToString("yyyyMMdd"));
			}
		}

		protected override string ValidationConfigFile
		{
			get
			{
				return Server.MapPath("~/ApplicationDevelopment/EntityXWorkTicket/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
			}
		}

		#endregion properties

		#region private methods

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var functionalityData = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(functionalityData, drpEntityList,
				StandardDataModel.StandardDataColumns.Name,
				EntityDataModel.DataColumns.EntityId);

			var WorkTicketData = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(WorkTicketData, drpWorkTicketList,
				StandardDataModel.StandardDataColumns.Name,
				WorkTicketDataModel.DataColumns.WorkTicketId);

			if (isTesting)
			{
				drpEntityList.AutoPostBack = true;
				drpWorkTicketList.AutoPostBack = true;

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
				if (drpWorkTicketList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtWorkTicketId.Text.Trim()))
					{
						drpWorkTicketList.SelectedValue = txtWorkTicketId.Text;
					}
					else
					{
						txtWorkTicketId.Text = drpWorkTicketList.SelectedItem.Value;
					}
				}


				txtEntityId.Visible = true;
				txtWorkTicketId.Visible = true;

			}
			else
			{

				if (!string.IsNullOrEmpty(txtEntityId.Text.Trim()))
				{
					drpEntityList.SelectedValue = txtEntityId.Text;
				}
				if (!string.IsNullOrEmpty(txtWorkTicketId.Text.Trim()))
				{
					drpWorkTicketList.SelectedValue = txtWorkTicketId.Text;
				}

				txtEntityId.Visible = false;
				txtWorkTicketId.Visible = false;

			}
		}

		public override void SetId(int setId, bool chkApplicationId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationId);
			txtEntityXWorkTicketId.Enabled = chkApplicationId;


		}

		public void LoadData(int entityXWorkTicketid, bool showId)
		{
			var data = new EntityXWorkTicketDataModel();
			data.EntityXWorkTicketId = entityXWorkTicketid;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				if (!showId)
				{
					txtEntityXWorkTicketId.Text = item.EntityXWorkTicketId.ToString();
					oHistoryList.Setup(PrimaryEntity, entityXWorkTicketid, PrimaryEntityKey);
				}
				else
				{
					txtEntityXWorkTicketId.Text = String.Empty;
				}


				txtEntityId.Text = item.EntityId.ToString();
				txtWorkTicketId.Text = item.WorkTicketId.ToString();
				txtAcknowledgedBy.Text = item.AcknowledgedBy.ToString();
				txtMemo.Text = item.Memo.ToString();
				txtKnowledgeDate.Text = item.KnowledgeDate.Value.ToString(SessionVariables.UserDateFormat);

				drpEntityList.SelectedValue = item.EntityId.ToString();
				drpWorkTicketList.SelectedValue = item.WorkTicketId.ToString();


				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
			}
			else
			{
				txtEntityId.Text = String.Empty;
				txtWorkTicketId.Text = String.Empty;
				txtAcknowledgedBy.Text = String.Empty;
				txtMemo.Text = String.Empty;
				txtKnowledgeDate.Text = String.Empty;


				drpEntityList.SelectedValue = "-1";
				drpWorkTicketList.SelectedValue = "-1";


			}
		}



		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetupDropdown();

				// CalendarExtenderKnowledgeDate.Format = SessionVariables.UserDateFormat;
				lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;
			}
			var isTesting = SessionVariables.IsTesting;
			txtEntityXWorkTicketId.Visible = isTesting;
			lblEntityXWorkTicketId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "EntityXWorkTicket";
			FolderLocationFromRoot = "/Shared/QualityAssurance";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityXWorkTicket;

			// set object variable reference            
			PlaceHolderCore = dynWorkTicketId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpEntityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtEntityId.Text = drpEntityList.SelectedItem.Value;
		}

		protected void drpWorkTicketList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtWorkTicketId.Text = drpWorkTicketList.SelectedItem.Value;
		}

		#endregion

	}
}