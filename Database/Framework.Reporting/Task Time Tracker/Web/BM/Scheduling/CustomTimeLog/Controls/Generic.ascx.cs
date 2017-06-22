using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog.Controls
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

		public int? CustomTimeLogId
		{
			get
			{
				if (txtCustomTimeLogId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtCustomTimeLogId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtCustomTimeLogId.Text);
				}
			}
			set
			{
				txtCustomTimeLogId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? PersonId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtPersonId.Text.Trim());
				else
					return int.Parse(drpPersonList.SelectedItem.Value);
			}
			set
			{
				txtPersonId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ApplicationId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtApplicationId.Text.Trim());
				else
					return int.Parse(drpApplicationList.SelectedItem.Value);
			}
			set
			{
				txtApplicationId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string CustomTimeCategory
		{
			get
			{
				return txtCustomTimeCategory.Text.Trim();
			}
			set
			{
				txtCustomTimeCategory.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int CustomTimeCategoryId
		{
			get
			{
				var dt = CustomTimeCategoryDataManager.GetEntityDetails(CustomTimeCategoryDataModel.Empty,SessionVariables.RequestProfile);
				return Convert.ToInt32(dt[0].CustomTimeCategoryId);
			}
			
		}

		public string CustomTimeLogKey
		{
			get
			{
				return txtCustomTimeLogKey.Text.Trim();
			}
			set
			{
				txtCustomTimeLogKey.Text = (value == null) ? String.Empty : value.ToString();
			}
		}
		public DateTime? WorkDate
		{
			get
			{
				return DateTime.ParseExact(txtWorkDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
			}
			set
			{
				txtWorkDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
			}
		}

			

		public decimal? NoofFilesPromoted
		{
			get
			{
				return decimal.Parse(txtNoofFilesPromoted.Text);
			}
			set
			{
				txtNoofFilesPromoted.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		
		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new CustomTimeLogDataModel();

			data.CustomTimeLogId = CustomTimeLogId;
			data.CustomTimeLogKey = CustomTimeLogKey;
			data.PersonId = PersonId;
			data.ApplicationId = ApplicationId;
			data.CustomTimeCategoryId = CustomTimeCategoryId;
			data.PromotedDate = WorkDate;
			data.Value = NoofFilesPromoted;
			

			if (action == "Insert")
			{
				var dtCustomTimeLog = CustomTimeLogDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtCustomTimeLog.Rows.Count == 0)
				{
					var customTimeLogId = CustomTimeLogDataManager.Create(data, SessionVariables.RequestProfile);										
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
            {
                CustomTimeLogDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.CustomTimeLogId;        

		}

			
		public override void SetId(int setId, bool chkCustomTimeLogId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkCustomTimeLogId);
			txtCustomTimeLogId.Enabled = chkCustomTimeLogId;

		}

		public void LoadData(int customTimeLogId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new CustomTimeLogDataModel();
			dataQuery.CustomTimeLogId = customTimeLogId;

			var items = CustomTimeLogDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			CustomTimeLogId = item.CustomTimeLogId;
			CustomTimeLogKey = item.CustomTimeLogKey;
			PersonId = item.PersonId;
			ApplicationId = item.ApplicationId;
            CustomTimeCategory = item.CustomTimeCategory;
			WorkDate = item.PromotedDate;
			NoofFilesPromoted = item.Value;
			

			if (!showId)
			{
				txtCustomTimeLogId.Text = item.CustomTimeLogId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)SystemEntity.CustomTimeLog, customTimeLogId, "CustomTimeLog");

			}
			else
			{
				txtCustomTimeLogId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new CustomTimeLogDataModel();

			CustomTimeLogId = data.CustomTimeLogId;
			CustomTimeLogKey = data.CustomTimeLogKey;
			PersonId = data.PersonId;
			ApplicationId = data.ApplicationId;
            CustomTimeCategory = data.CustomTimeCategory;
			NoofFilesPromoted = data.Value;
			WorkDate = data.PromotedDate;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var personData = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(personData, drpPersonList, ApplicationUserDataModel.DataColumns.FirstName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);

			drpPersonList.SelectedValue = SessionVariables.RequestProfile.AuditId.ToString();

			var applicationData = ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(applicationData, drpApplicationList, ApplicationDataModel.DataColumns.Name,
					ApplicationDataModel.DataColumns.ApplicationId);

			if (isTesting)
			{
				//drpPersonList.AutoPostBack = true;
				

				if (drpPersonList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
					{
						drpPersonList.SelectedValue = txtPersonId.Text;
					}
					else
					{
						txtPersonId.Text = drpPersonList.SelectedItem.Value;
					}
				}

				if (drpApplicationList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
					{
						drpApplicationList.SelectedValue = txtApplicationId.Text;
					}
					else
					{
						txtApplicationId.Text = drpApplicationList.SelectedItem.Value;
					}
				}
				txtPersonId.Visible = true;
				txtApplicationId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
				{
					drpPersonList.SelectedValue = txtPersonId.Text;
				}
				if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
				{
					drpApplicationList.SelectedValue = txtApplicationId.Text;
				}
			}

		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "CustomTimeLog";
			FolderLocationFromRoot = "Scheduling/CustomTimeLog";
			PrimaryEntity = SystemEntity.Schedule;

			// set object variable reference            
			PlaceHolderCore = dynCustomTimeLogId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetupDropdown();
				//WorkDate_CalendarExtender.Format = SessionVariables.UserDateFormat;
				lblWorkDateFormat.Text = SessionVariables.UserDateFormat;				
			}

			var isTesting = SessionVariables.IsTesting;
			txtCustomTimeLogId.Visible = isTesting;
			lblCustomTimeLogId.Visible = isTesting;
		}

		protected void drpPersonList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtPersonId.Text = drpPersonList.SelectedItem.Value;
		}

		protected void drpApplicationList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationId.Text = drpApplicationList.SelectedItem.Value;
		}


		
		#endregion

	}
}