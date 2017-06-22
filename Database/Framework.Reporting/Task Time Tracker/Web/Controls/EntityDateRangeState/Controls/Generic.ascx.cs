using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;
using System.Globalization;

namespace ApplicationContainer.UI.Web.EntityDateRangeState.Controls
{
	public partial class Generic : ControlGeneric
	{
		#region properties

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

		public int? EntityDateRangeStateId
		{
			get
			{
				if (txtEntityDateRangeStateId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtEntityDateRangeStateId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtEntityDateRangeStateId.Text);
				}
			}
		}

		public DateTime? StartDate
		{
			get
			{
				return Convert.ToDateTime(txtStartDate.Text);
			}
			set
			{
				txtStartDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);				
			}
		}

		public DateTime? EndDate
		{
			get
			{
				return Convert.ToDateTime(txtEndDate.Text);
			}
			set
			{
				txtEndDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);				
			}
			
		}

		public int? SystemEntityId
		{
			get
			{
				if (txtSystemEntityId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtSystemEntityId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtSystemEntityId.Text);
				}
			}
		}

		public int? KeyId
		{
			get
			{
				if (txtKeyId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtKeyId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtKeyId.Text);
				}
			}
		}

		public int? EntityDateRangeStateTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;

				if (isTesting)
					return int.Parse(txtEntityDateRangeStateTypeId.Text.Trim());
				else
					return int.Parse(drpEntityDateRangeStateTypeList.SelectedItem.Value);
			}
			set
			{
				txtEntityDateRangeStateTypeId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Notes
		{
			get
			{
				return txtNotes.Text;
			}
		}

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new EntityDateRangeStateDataModel();

			data.EntityDateRangeStateId = EntityDateRangeStateId;
			data.EntityDateRangeStateTypeId = EntityDateRangeStateTypeId;
			data.StartDate = StartDate;
			data.EndDate = EndDate;
			data.KeyId = KeyId;
			data.SystemEntityId = SystemEntityId;
			data.Notes = Notes;

			if (action == "Insert")
			{
                var dtDateRangeState = EntityDateRangeStateDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtDateRangeState.Rows.Count == 0)
				{
                    EntityDateRangeStateDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                EntityDateRangeStateDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of EntityDateRangeStateId ?
			return data.EntityDateRangeStateId;
		}

		public override void SetId(int setId, bool chkEntityDateRangeStateId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkEntityDateRangeStateId);
			txtEntityDateRangeStateId.Enabled = chkEntityDateRangeStateId;
			//txtDescription.Enabled = !chkMilestoneId;
			//txtName.Enabled = !chkMilestoneId;
			//txtSortOrder.Enabled = !chkMilestoneId;
			//drpProjectList.Enabled = !chkMilestoneId;
			//txtProjectId.Enabled = !chkMilestoneId;
		}

		public void LoadData(int entityDateRangeStateId, bool showId)
		{
			Clear();

			var data = new EntityDateRangeStateDataModel();

			data.EntityDateRangeStateId = entityDateRangeStateId;
            var items = EntityDateRangeStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			txtEntityDateRangeStateId.Text       = item.EntityDateRangeStateId.ToString();
			txtEntityDateRangeStateTypeId.Text   = item.EntityDateRangeStateTypeId.ToString();
			txtEndDate.Text                      = item.EndDate.ToString();
			txtStartDate.Text                    = item.StartDate.ToString();
			txtKeyId.Text                        = item.KeyId.ToString();
			txtSystemEntityId.Text               = item.SystemEntityId.ToString();
			txtNotes.Text                        = item.Notes.ToString();

			if (!showId)
			{
				txtEntityDateRangeStateId.Text = item.EntityDateRangeStateId.ToString();
				oHistoryList.Setup(PrimaryEntity, entityDateRangeStateId, PrimaryEntityKey);
			}
			else
			{
				txtEntityDateRangeStateId.Text = string.Empty;
			}
			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var data = new EntityDateRangeStateTypeDataModel();
			var dateRangeStateTypeData = EntityDateRangeStateTypeDataManager.GetDetails(EntityDateRangeStateTypeDataModel.Empty,SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(dateRangeStateTypeData, drpEntityDateRangeStateTypeList, StandardDataModel.StandardDataColumns.Name, EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId);

			if (isTesting)
			{
				drpEntityDateRangeStateTypeList.AutoPostBack = true;
				if (drpEntityDateRangeStateTypeList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtEntityDateRangeStateTypeId.Text.Trim()))
					{
						drpEntityDateRangeStateTypeList.SelectedValue = txtEntityDateRangeStateTypeId.Text;
					}
					else
					{
						txtEntityDateRangeStateTypeId.Text = drpEntityDateRangeStateTypeList.SelectedItem.Value;
					}
				}
				txtEntityDateRangeStateTypeId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtEntityDateRangeStateTypeId.Text.Trim()))
				{
					drpEntityDateRangeStateTypeList.SelectedValue = txtEntityDateRangeStateTypeId.Text;
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
				txtEntityDateRangeStateId.Visible = isTesting;
				lblEntityDateRangeStateId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.EntityDateRangeState;
			PrimaryEntityKey = "EntityDateRangeState";
			FolderLocationFromRoot = "EntityDateRangeState";

			// set object variable reference            
			PlaceHolderCore = dynEntityDateRangeStateId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpEntityDateRangeStateTypeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtEntityDateRangeStateTypeId.Text = drpEntityDateRangeStateTypeList.SelectedItem.Value;
		}

		#endregion
	}
}