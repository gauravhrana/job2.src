using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Framework.Components;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Globalization;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;


namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
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

		public int? ScheduleId
		{
			get
			{
				if (txtScheduleId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtScheduleId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtScheduleId.Text);
				}
			}
			set
			{
				txtScheduleId.Text = (value == null) ? String.Empty : value.ToString();
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

		public int? ScheduleStateId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtScheduleState.Text.Trim());
				else
					return int.Parse(drpScheduleStateList.SelectedItem.Value);
			}
			set
			{
				txtScheduleState.Text = (value == null) ? String.Empty : value.ToString();
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

		public DateTime? StartTime
		{
			get
			{
				DateTime t = Convert.ToDateTime(txtStartTime.Text.Trim());
				DateTime d = DateTime.ParseExact(txtWorkDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
				DateTime dtCombined = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);

				return dtCombined;
			}
			set
			{			
				txtStartTime.Text = (value == null) ? String.Empty : value.Value.ToString("t");				
			}
		}

		public DateTime? EndTime
		{
			get
			{
				DateTime t = Convert.ToDateTime(txtEndTime.Text.Trim());
				DateTime d = DateTime.ParseExact(txtWorkDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
				DateTime dtCombined = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);

				return dtCombined;
				
			}
			set
			{
				txtEndTime.Text = (value == null) ? String.Empty : value.Value.ToString("t");				
				
			}
		}

		public decimal? TotalHoursWorked
		{
			get
			{
				return decimal.Parse(txtTotalHoursWorked.Text);
			}
			set
			{
				txtTotalHoursWorked.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public decimal? PlannedHours
		{
			get
			{
				return decimal.Parse(txtPlannedHours.Text);
			}
			set
			{
				txtPlannedHours.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public DateTime? NextWorkDate
		{
			get
			{
				return DateTime.ParseExact(txtNextWorkDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
			}
			set
			{
				txtNextWorkDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);				
			}

		}

		public DateTime? NextWorkTime
		{
			get
			{
				var t = Convert.ToDateTime(txtNextWorkTime.Text.Trim());
				var d = DateTime.ParseExact(txtNextWorkDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
				var dtCombined = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);

				return dtCombined;
			}
			set
			{
				//txtNextWorkTime.Text = (value == null) ? String.Empty : value.ToString();
				txtNextWorkTime.Text = (value == null) ? String.Empty : value.Value.ToString("t");				
			}
		}

		public int? CreatedByAuditId
		{
			get
			{
				return int.Parse(txtCreatedByAuditId.Text);
			}
			set
			{
				txtCreatedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ModifiedByAuditId
		{
			get
			{
				return int.Parse(txtModifiedByAuditId.Text);
			}
			set
			{
				txtModifiedByAuditId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public DateTime? CreatedDate
		{
			get
			{

				return DateTime.Parse(DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo));

				//return DateTime.ParseExact(txtCreatedDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
				////	return DateTime.Parse(txtCreatedDate.Text.Trim());
			}
			set
			{

				txtCreatedDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
			}
		}
		//	//get
		//	//{
		//	//	//DateTime dt = Convert.ToDateTime(txtCreatedDate.Text.Trim());
		//	//	//return DateTime.ParseExact(dt.ToString(this.DateTimeFormat), this.DateTimeFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
		//	//	DateTime dt = Convert.ToDateTime(txtCreatedDate.Text.Trim());
		//	//	return DateTime.Parse(dt.ToString("t"), null);
		//	//}
		//	//set
		//	//{
		//	//	txtCreatedDate.Text = (value == null) ? String.Empty : value.Value.ToString("t");				
		//	//}
		//}

		public DateTime? ModifiedDate
		{
			get
			{
				var dt = Convert.ToDateTime(txtModifiedDate.Text.Trim());
				return DateTime.ParseExact(dt.ToString(SessionVariables.UserDateFormat), SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);
			}
			set
			{
				txtModifiedDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
			}
		}

		#endregion properties		

		#region private methods

		public override int? Save(string action)
		{
			var data = new ScheduleDataModel();

			data.ScheduleId		  = ScheduleId;
			data.Person			  = PersonId.ToString() ;
			data.ScheduleStateId  = ScheduleStateId;
			data.WorkDate		  = WorkDate;
			data.StartTime        = StartTime;
			data.EndTime          = EndTime;
			data.NextWorkDate	  = NextWorkDate;
			data.NextWorkTime	  = NextWorkTime;
			data.TotalHoursWorked = TotalHoursWorked;
			data.PlannedHours	  = PlannedHours;
			data.FromSearchDate   = WorkDate;
			data.ToSearchDate     = WorkDate;

            if(NextWorkDate < WorkDate)
			{
				throw new Exception("Next Work Date should always be greater than Work Date.");
			}
            else if (StartTime > EndTime && EndTime != WorkDate) // JIRA #4097 validation
            {
                throw new Exception("End Time should always be greater than Start Time");
            }
            else
            {
                if (action == "Insert")
                {
                    var dtSchedule = ScheduleDataManager.DoesExist(data, SessionVariables.RequestProfile);

                    if (dtSchedule.Rows.Count == 0)
                    {
                        // create method itself will return newly created schedule Id
                        var scheduleId = ScheduleDataManager.Create(data, SessionVariables.RequestProfile);
                        data.ScheduleId = scheduleId;

                        // add default schedule questions
                        ApplicationContainer.UI.Web.App_Workflow.ScheduleWorkflow.AddScheduleQuestions(scheduleId, SessionVariables.RequestProfile);

                    }
                    else
                    {
                        throw new Exception("Record with given ID already exists.");
                    }
                }
                else
                {
                    data.CreatedDate = CreatedDate;
                    ScheduleDataManager.Update(data, SessionVariables.RequestProfile);
                }

            }
			
			// not correct ... when doing insert, we didn't get/change the value of ScheduleID ?
			return data.ScheduleId;

		}

		public override void SetId(int setId, bool chkScheduleId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkScheduleId);
			txtScheduleId.Enabled = chkScheduleId;
			
		}

		public void LoadData(int scheduleId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new ScheduleDataModel();
			dataQuery.ScheduleId = scheduleId;

            var items = ScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			ScheduleId			= item.ScheduleId;
			PersonId			= item.PersonId;
			ScheduleStateId		= item.ScheduleStateId;
			WorkDate			= item.WorkDate;
			StartTime			= item.StartTime;
			EndTime				= item.EndTime;
			NextWorkDate		= item.NextWorkDate;
			NextWorkTime		= item.NextWorkTime;
			TotalHoursWorked	= item.TotalHoursWorked;
			PlannedHours		= item.PlannedHours;
			CreatedByAuditId	= item.CreatedByAuditId;
			ModifiedByAuditId	= item.ModifiedByAuditId;
			CreatedDate			= item.CreatedDate;
			ModifiedDate		= item.ModifiedDate;

			if (!showId)
			{
				txtScheduleId.Text = item.ScheduleId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)SystemEntity.Schedule, scheduleId, "Schedule");

			}
			else
			{
				txtScheduleId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ScheduleDataModel();

			ScheduleId			= data.ScheduleId;
			PersonId			= data.PersonId;
			WorkDate			= data.WorkDate;
			StartTime			= data.StartTime;
			EndTime				= data.EndTime;
			NextWorkDate		= data.NextWorkDate;
			NextWorkTime		= data.NextWorkTime;
			TotalHoursWorked	= data.TotalHoursWorked;
			PlannedHours		= data.PlannedHours;
			//CreatedDate = data.CreatedDate;
			//ModifiedDate = data.ModifiedDate;
			//CreatedByAuditId = data.CreatedByAuditId;
			//ModifiedByAuditId = data.ModifiedByAuditId;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var personData = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(personData, drpPersonList, ApplicationUserDataModel.DataColumns.FirstName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);
			
			drpPersonList.SelectedValue = SessionVariables.RequestProfile.AuditId.ToString();

            var scheduleStateData = ScheduleStateDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(scheduleStateData, drpScheduleStateList, StandardDataModel.StandardDataColumns.Name,
					ScheduleStateDataModel.DataColumns.ScheduleStateId);

			if (drpScheduleStateList.Items.FindByText("Submitted") != null)
			{
				drpScheduleStateList.SelectedValue = drpScheduleStateList.Items.FindByText("Submitted").Value; // Submitted selected by default
			}

			if (isTesting)
			{
				drpPersonList.AutoPostBack = true;
				drpScheduleStateList.AutoPostBack = true;

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


				if (drpScheduleStateList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtScheduleState.Text.Trim()))
					{
						drpScheduleStateList.SelectedValue = txtScheduleState.Text;
					}
					else
					{
						txtScheduleState.Text = drpScheduleStateList.SelectedItem.Value;
					}
				}
				txtPersonId.Visible = true;
				txtScheduleState.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
				{
					drpPersonList.SelectedValue = txtPersonId.Text;
				}

				if (!string.IsNullOrEmpty(txtScheduleState.Text.Trim()))
				{
					drpScheduleStateList.SelectedValue = txtScheduleState.Text;
				}
			}

		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "Schedule";
			FolderLocationFromRoot = "Scheduling/Schedule";
			PrimaryEntity = SystemEntity.Schedule;

			// set object variable reference            
			PlaceHolderCore = dynScheduleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				SetupDropdown();                
                
				//WorkDate_CalendarExtender.Format     = SessionVariables.UserDateFormat;
				//NextWorkDate_CalendarExtender.Format = SessionVariables.UserDateFormat;				

                lblWorkDateFormat.Text               = SessionVariables.UserDateFormat;
                lblNextWorkDateFormat.Text           = SessionVariables.UserDateFormat;
				
            }
			if (Session["msg"] != null)
			{
				string message =Session["msg"].ToString();
				Response.Write(message);
			}

			var isTesting = SessionVariables.IsTesting;
			txtScheduleId.Visible = isTesting;
			lblScheduleId.Visible = isTesting;			
        }

        protected void drpPersonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPersonId.Text = drpPersonList.SelectedItem.Value;
        }

		protected void drpScheduleStateList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtScheduleState.Text = drpScheduleStateList.SelectedItem.Value;
		}

		
		#endregion

	}
}