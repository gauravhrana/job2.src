using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.ApplicationUser;
using Shared.UI.Web.Controls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.BM.Scheduling
{
	public partial class WorkCategory : BasePage
	{
		public class ResultSet
		{
			string category;
			int value;
			string week;
			

			public ResultSet( string week, string category, int value)
			{
				Category = category;
				Value = value;
				Week = week;
			}

			public string Category
			{
				get
				{
					return category;
				}
				set
				{
					category = value;
				}
			}

			public int Value
			{
				get
				{
					return value;
				}
				set
				{
					this.value = value;
				}
			}

			public string Week
			{

				get
				{
					return week;
				}
				set
				{
					week = value;
				}
			}

			
		}


		DateRangeControl oDateRange = new DateRangeControl();

		protected void Page_Load(object sender, EventArgs e)
		{
			SetUpDateRangeControl();
			

			if (!IsPostBack)
			{
				var auData1 = CustomTimeCategoryDataManager.GetList(SessionVariables.RequestProfile);

				UIHelper.LoadDropDown(auData1, drpCustomTimeCategory, CustomTimeCategoryDataModel.DataColumns.Name,
					CustomTimeCategoryDataModel.DataColumns.CustomTimeCategoryId);
				drpApplicationUser.Items.Insert(0, "All");

				var auData = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);

				UIHelper.LoadDropDown(auData, drpApplicationUser, ApplicationUserDataModel.DataColumns.ApplicationUserName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);
				drpApplicationUser.Items.Insert(0, "All");

				txtPersonId.Visible = false;

			}
		}
		private void SetUpDateRangeControl()
		{
			//DateRangeControl oDateRange = new DateRangeControl();

			if (plcControlHolder != null)
			{
				oDateRange = (DateRangeControl)Page.LoadControl(ApplicationCommon.DateRangeControlPath);
				oDateRange.ID = "oDateRange";

				var dtPanel = new Panel();

				dtPanel.ID = "datepanel";
				//dtPanel.CssClass = "datepanel col-sm-10";

				dtPanel.Controls.Add(oDateRange);

				plcControlHolder.Controls.Add(dtPanel);


				//datepanel = dtPanel;
			}


			var funccall = "Fillup" + oDateRange.GetKey() + "();";
			oDateRange.DateRangeDropDown.Attributes.Add("onchange", funccall);
			oDateRange.HideLabel();


		}

		protected void drpApplicationUser_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationUserId.Text = drpApplicationUser.SelectedValue;
		}

		protected void drpGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		protected void drpSubGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		protected void drpCustomTimeCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtCustomTimeCategoryId.Text = drpCustomTimeCategory.SelectedValue;
		}

		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{
			var data = new ApplicationUserDataModel();
			data.ApplicationUserId = int.Parse(drpApplicationUser.SelectedValue);
			var dt = ApplicationUserDataManager.GetDetails(data, SessionVariables.RequestProfile);
			txtPersonId.Text = dt.Rows[0][ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
			txtPersonId.Visible = true;
			
			var	result = GetWorkCategoryDetails(int.Parse(drpApplicationUser.SelectedValue));
			var groupedList = result
								.GroupBy(u =>  u.Week)
								.Select(grp => grp.ToList())
								.ToList();
			var categorizedList = new List<ResultSet>();
			var finalList = new List<List<ResultSet>>();
			for (var i = 0; i < groupedList.Count; i++)
			{
				categorizedList = groupedList[i]
								.GroupBy(u => u.Category)
								.Select(grp => new ResultSet("", "", 0)
								{
									Category = grp.First().Category,
									Week = grp.First().Week,
									Value = grp.Sum(u1 => u1.Value),
								}).ToList();

				finalList.Add(categorizedList);				
				
			}
			rpt.DataSource = finalList.ToList();
			rpt.DataBind();			
		}
	

		protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var gv = (GridView)e.Item.FindControl("grd");
				var lbl = (Label)e.Item.FindControl("lblWeek");
				var fromDateTime = Convert.ToDateTime(DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.FromDateTime));

				if (gv != null && lbl != null)
				{

					var drv = (List<ResultSet>)e.Item.DataItem;

					if (!drv[0].Week.Equals(string.Empty))
					{
						var startdate = FirstDateOfWeek(fromDateTime.Year, int.Parse(drv[0].Week), CalendarWeekRule.FirstFourDayWeek);
						lbl.Text = "Week " + startdate + " - " + startdate.AddDays(7);
						gv.DataSource = drv;
						gv.DataBind();
					}
				}
				
			}
		}


		protected void btnReset_OnClick(object sender, EventArgs e)
		{
		}

		static DateTime FirstDateOfWeek(int year, int weekNum, CalendarWeekRule rule)
		{
			Debug.Assert(weekNum >= 1);

			DateTime jan1 = new DateTime(year, 1, 1);

			int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
			DateTime firstMonday = jan1.AddDays(daysOffset);
			Debug.Assert(firstMonday.DayOfWeek == DayOfWeek.Monday);

			var cal = CultureInfo.CurrentCulture.Calendar;
			int firstWeek = cal.GetWeekOfYear(firstMonday, rule, DayOfWeek.Monday);

			if (firstWeek <= 1)
			{
				weekNum -= 1;
			}

			DateTime result = firstMonday.AddDays(weekNum * 7);

			return result;
		}
		
		public List<ResultSet> GetWorkCategoryDetails(int appUserId)
		{
			var appUser = new ScheduleDataModel();			
			appUser.PersonId = appUserId;			
			var category = string.Empty;
			
			var result = new ArrayList();
			var fromDateTime = new DateTime();
			var toDateTime = new DateTime();
			
			
			fromDateTime = Convert.ToDateTime(DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.FromDateTime));
			toDateTime = Convert.ToDateTime(DateTimeHelper.FromUserDateFormatToApplicationDateFormat(oDateRange.ToDateTime));
		
			var format = SessionVariables.UserDateFormat;
			var fromDate = fromDateTime.ToString(format);
			var toDate = toDateTime.ToString(format);
			var scheduleDetails = new ScheduleDetailDataModel();
			appUser.FromSearchDate = Convert.ToDateTime(fromDate);
			appUser.ToSearchDate = Convert.ToDateTime(toDate);
			
			var resultList = new List<ResultSet>();
			var data = new CustomTimeLogDataModel();
			data.PersonId = appUserId;
			data.FromSearchDate = Convert.ToDateTime(fromDate);
			data.ToSearchDate = Convert.ToDateTime(toDate);
			var list = CustomTimeLogDataManager.GetWorkCategoryList(data, SessionVariables.RequestProfile);
			
			var categoryDetails = list[0].Category;
			var week = list[0].Week;
			var value = list[0].Value;
			var weekCount = 0;

			for (var i = 0; i < list.Count; i++)
			{
				var resultSet = new ResultSet("", "", 0);
				if (week.Equals(list[i].Week))
				{
					
					resultSet.Category = list[i].Category;
					resultSet.Value =  list[i].Value;
					resultSet.Week = list[i].Week.ToString();			
					weekCount++;
				}
				else
				{
					weekCount = 0;	
					week = list[i].Week;
					
				}
				//result.Add(resultSet);
				resultList.Add(resultSet);
				//result = new ArrayList();
			}

			return resultList;
			
			
		}
		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "WorkCategoryDefaultView";
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();
		}

	}
}