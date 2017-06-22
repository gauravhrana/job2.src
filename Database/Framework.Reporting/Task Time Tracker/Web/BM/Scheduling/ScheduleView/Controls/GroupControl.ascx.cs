using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.ApplicationUser;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;


namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
	public partial class GroupControl : BaseControl
	{
		public event EventHandler OnSearch;
		public event EventHandler OnGroup;

		public string SelectedCategory
		{
			get { return GroupByCategory.SelectedValue; }
			set { GroupByCategory.SelectedValue = value; }
		}

		public string DateTimeFormat
		{
			get
			{
                return SessionVariables.UserDateFormat;
			}
		}

		public ScheduleDataModel SearchParameters
		{
			get
			{
				var data = new ScheduleDataModel();

				
				return data;
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
			}

		}
		protected void drpSearchConditionPerson_SelectedIndexChanged(object sender, EventArgs e)
		{
			RaiseSearch();

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			RaiseSearch();
			RaiseGroup();
		}

		public void RaiseSearch()
		{
			if (OnSearch != null)
			{
				OnSearch(this, EventArgs.Empty);
			}
		}

		public void RaiseGroup()
		{
			if (OnGroup != null)
			{
				OnGroup(this, EventArgs.Empty);
			}
		}

		public string GroupByPerson()
		{
			SessionVariables.RequestProfile.AuditId = 200;
			var dt = ScheduleDataManager.GetList(SessionVariables.RequestProfile);
	        
			var workSummary = from eschedule in dt.AsEnumerable()

			group eschedule by new {PersonId = eschedule.Field<int>("PersonId")} into wsum

			select new

			{
				PersonId =  wsum.Key.PersonId,

				Duration = wsum.Sum(eschedule => eschedule.Field<decimal>("TotalHoursWorked")),

				//AvgStartTime = decimal.Round(wsum.Average(eschedule => eschedule.Field<decimal>("StartTime")), 2),
                AvgStartTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("StartTime").Hour + "." + eschedule.Field<DateTime>("StartTime").Minute), 2)),

				//AvgEndTime = decimal.Round(wsum.Average(eschedule => eschedule.Field<decimal>("EndTime")), 2)
                AvgEndTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("EndTime").Hour + "." + eschedule.Field<DateTime>("EndTime").Minute), 2)),
			};

			foreach (var VARIABLE in workSummary)
			{
				var personId = VARIABLE.GetType().GetProperty("PersonId").GetValue(VARIABLE, null).ToString();
				var noofhours = VARIABLE.GetType().GetProperty("Duration").GetValue(VARIABLE, null).ToString();
				var starttime = VARIABLE.GetType().GetProperty("AvgStartTime").GetValue(VARIABLE, null).ToString();
				var endtime = VARIABLE.GetType().GetProperty("AvgEndTime").GetValue(VARIABLE, null).ToString();
				

				if (int.Parse(personId) != 0)
				{
					var personData = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
					var fullname = (from DataRow dr in personData.Rows
					                   where (int) dr["ApplicationUserId"] == int.Parse(personId)
					                   select (string) dr["FullName"]).FirstOrDefault();

					var summary = "Person " + fullname + "'s Total Hours Worked: " + noofhours +
					                "hours,  Average Start Time: " + starttime +
									" hours,  Average End Time: " + endtime +
									" hours.";
					RaiseSearch();

					return summary;
				}
			}
			return "";

		}

		public DataTable GroupByDay()
		{
			SessionVariables.RequestProfile.AuditId = 200;
			var dt = ScheduleDataManager.GetList(SessionVariables.RequestProfile);

			var workSummary = from eschedule in dt.AsEnumerable()

			let WorkDate = eschedule.Field<DateTime>("WorkDate")

			group eschedule by new {Day = WorkDate.Day} into wsum

			select new

			{
				WorkDay = wsum.Key.Day,

				Duration = wsum.Sum(eschedule => eschedule.Field<decimal>("TotalHoursWorked")),

				//AvgStartTime = decimal.Round(wsum.Average(eschedule => eschedule.Field<decimal>("StartTime")), 2),
                AvgStartTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("StartTime").Hour + "." + eschedule.Field<DateTime>("StartTime").Minute), 2)),

                AvgEndTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("EndTime").Hour + "." + eschedule.Field<DateTime>("EndTime").Minute), 2)),
			
			};
			
			var _dt = new DataTable();
			var values = new object[4];
			_dt.Columns.Add("WorkDay");
			_dt.Columns.Add("Duration");
			_dt.Columns.Add("AverageStartTime");
			_dt.Columns.Add("AverageEndTime");
			foreach (var VARIABLE in workSummary)
			{
				var workday = VARIABLE.GetType().GetProperty("WorkDay").GetValue(VARIABLE, null).ToString();
				var noofhours = VARIABLE.GetType().GetProperty("Duration").GetValue(VARIABLE, null).ToString();
				var starttime = decimal.Round(decimal.Parse( VARIABLE.GetType().GetProperty("AvgStartTime").GetValue(VARIABLE, null).ToString()), 2).ToString();
				var endtime = decimal.Round(decimal.Parse( VARIABLE.GetType().GetProperty("AvgEndTime").GetValue(VARIABLE, null).ToString()), 2).ToString();

				values[0] = workday;
				values[1] = noofhours;
				values[2] = starttime;
				values[3] = endtime;

				_dt.Rows.Add(values);
					
				
			}
            RaiseSearch();
            _dt.DefaultView.Sort = "WorkDay ASC";
            return _dt.DefaultView.ToTable();

		}

		public DataTable GroupByMonth()
		{
			SessionVariables.RequestProfile.AuditId = 200;
			var dt = ScheduleDataManager.GetList(SessionVariables.RequestProfile);

			var workSummary = from eschedule in dt.AsEnumerable()

			let WorkDate = eschedule.Field<DateTime>("WorkDate")

			group eschedule by new { Month = WorkDate.Month } into wsum

			select new

			{
				Month = wsum.Key.Month,

				Duration = wsum.Sum(eschedule => eschedule.Field<decimal>("TotalHoursWorked")),

                AvgStartTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("StartTime").Hour + "." + eschedule.Field<DateTime>("StartTime").Minute), 2)),

                AvgEndTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("EndTime").Hour + "." + eschedule.Field<DateTime>("EndTime").Minute), 2)),
			
			};

			var _dt = new DataTable();
			var values = new object[4];
			_dt.Columns.Add("Month");
			_dt.Columns.Add("Duration");
			_dt.Columns.Add("AverageStartTime");
			_dt.Columns.Add("AverageEndTime");
			foreach (var VARIABLE in workSummary)
			{
				var month = VARIABLE.GetType().GetProperty("Month").GetValue(VARIABLE, null).ToString();
				var noofhours = VARIABLE.GetType().GetProperty("Duration").GetValue(VARIABLE, null).ToString();
                var starttime = decimal.Round(decimal.Parse(VARIABLE.GetType().GetProperty("AvgStartTime").GetValue(VARIABLE, null).ToString()), 2).ToString();
                var endtime = decimal.Round(decimal.Parse(VARIABLE.GetType().GetProperty("AvgEndTime").GetValue(VARIABLE, null).ToString()), 2).ToString();

				values[0] = month;
				values[1] = noofhours;
				values[2] = starttime;
				values[3] = endtime;

				_dt.Rows.Add(values);


			}
            _dt.DefaultView.Sort = "Month ASC";
            return _dt.DefaultView.ToTable();

		}

		public DataTable GroupByYear()
		{
			SessionVariables.RequestProfile.AuditId = 200;
			var dt = ScheduleDataManager.GetList(SessionVariables.RequestProfile);

			var workSummary = from eschedule in dt.AsEnumerable()

			let WorkDate = eschedule.Field<DateTime>("WorkDate")

			group eschedule by new { Year = WorkDate.Year } into wsum

			select new

			{
				Year = wsum.Key.Year,

				Duration = wsum.Sum(eschedule => eschedule.Field<decimal>("TotalHoursWorked")),


                AvgStartTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("StartTime").Hour + "." + eschedule.Field<DateTime>("StartTime").Minute), 2)),

                AvgEndTime = wsum.Average(eschedule => decimal.Round(Convert.ToDecimal(eschedule.Field<DateTime>("EndTime").Hour + "." + eschedule.Field<DateTime>("EndTime").Minute), 2)),
			

			};

			var _dt = new DataTable();
			var values = new object[4];
			_dt.Columns.Add("Year");
			_dt.Columns.Add("Duration");
			_dt.Columns.Add("AverageStartTime");
			_dt.Columns.Add("AverageEndTime");
			foreach (var VARIABLE in workSummary)
			{
				var year = VARIABLE.GetType().GetProperty("Year").GetValue(VARIABLE, null).ToString();
                var noofhours = VARIABLE.GetType().GetProperty("Duration").GetValue(VARIABLE, null).ToString();
                var starttime = decimal.Round(decimal.Parse(VARIABLE.GetType().GetProperty("AvgStartTime").GetValue(VARIABLE, null).ToString()), 2).ToString();
                var endtime = decimal.Round(decimal.Parse(VARIABLE.GetType().GetProperty("AvgEndTime").GetValue(VARIABLE, null).ToString()), 2).ToString();

				values[0] = year;
				values[1] = noofhours;
				values[2] = starttime;
				values[3] = endtime;

				_dt.Rows.Add(values);


			}

            _dt.DefaultView.Sort = "Year ASC";
			return _dt.DefaultView.ToTable();

		}

	}
}