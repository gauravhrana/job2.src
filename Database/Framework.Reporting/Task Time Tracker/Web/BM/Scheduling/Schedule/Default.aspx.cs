using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.ReleaseLog;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Collections;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;
//using ApplicationContainer.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
	public partial class Default : PageDefault
    {
		protected override DataTable GetData()
        {
            var dt = ScheduleDataManager.Search(oSearchFilter.SearchParameters,SessionVariables.RequestProfile);

			if (GroupBy.Equals("WorkDate–Day") || SubGroupBy.Equals("WorkDate–Day"))
			{
				dt.Columns.Add("WorkDate–Day", typeof(Int32));

				foreach (DataRow dr in dt.Rows)
				{
					var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
					dr["WorkDate–Day"] = workdate.Day;
				}
			}
			else if (GroupBy.Equals("WorkDate–Month") || SubGroupBy.Equals("WorkDate–Month"))
			{
				dt.Columns.Add("WorkDate–Month", typeof(String));

				foreach (DataRow dr in dt.Rows)
				{
					var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
					var mfi = new DateTimeFormatInfo();
					var strMonthName = mfi.GetAbbreviatedMonthName(workdate.Month).ToString();
					dr["WorkDate–Month"] = strMonthName;
				}
			}
			else if (GroupBy.Equals("WorkDate–Year") || SubGroupBy.Equals("WorkDate–Year"))
			{
				dt.Columns.Add("WorkDate–Year", typeof(Int32));

				foreach (DataRow dr in dt.Rows)
				{
					var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
					dr["WorkDate–Year"] = workdate.Year;
				}
			}
			else if (GroupBy.Equals("WorkDate–Week") || SubGroupBy.Equals("WorkDate–Week"))
			{
				dt.Columns.Add("WorkDate–Week", typeof(Int32));

				foreach (DataRow dr in dt.Rows)
				{
					var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
					dr["WorkDate–Week"] = workdate.DayOfWeek;
				}
			}
			else if (GroupBy.Equals("WorkDate–Quarter") || SubGroupBy.Equals("WorkDate–Quarter"))
			{
				dt.Columns.Add("WorkDate–Quarter", typeof(String));

				foreach (DataRow dr in dt.Rows)
				{
					var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
					var quarter = ((workdate.Month - 3) % 12) / 4;

					switch (quarter)
					{
						case 1:
							dr["WorkDate–Quarter"] = "Q1";
							break;
						case 2:
							dr["WorkDate–Quarter"] = "Q2";
							break;
						case 3:
							dr["WorkDate–Quarter"] = "Q3";
							break;
						case 4:
							dr["WorkDate–Quarter"] = "Q4";
							break;
					}
				}
			}

			dt.AcceptChanges();
			return dt;
        }

		
		//void oSearchFilter_OnGroup(object sender, EventArgs e)
		//{
		//	if (GroupControl1.SelectedCategory.Equals("Person"))
		//	{
		//		summary.Text = GroupControl1.GroupByPerson();
		//		summary.Visible = true;
		//		DVGroupByDay.Visible = false;
		//		DVGroupByMonth.Visible = false;
		//		DVGroupByYear.Visible = false;
		//		lblSummaryLine.Visible = false;
		//	}
		//	if (GroupControl1.SelectedCategory.Equals("Day"))
		//	{
		//		SearchFilterCore.GroupBy = "Day";
		//		DVGroupByDay.DataSource = GroupControl1.GroupByDay();
		//		DVGroupByDay.DataBind();
		//		DVGroupByDay.Visible = true;
		//		DVGroupByMonth.Visible = false;
		//		DVGroupByYear.Visible = false;
		//		summary.Visible = false;
		//		PopulateSummaryLine(GroupControl1.GroupByDay(), DVGroupByDay, "Day");
                
		//	}
		//	if (GroupControl1.SelectedCategory.Equals("Month"))
		//	{
		//		SearchFilterCore.GroupBy = "Month";
		//		DVGroupByMonth.DataSource = GroupControl1.GroupByMonth();
		//		DVGroupByMonth.DataBind();
		//		DVGroupByDay.Visible = false;
		//		DVGroupByMonth.Visible = true;
		//		DVGroupByYear.Visible = false;
		//		summary.Visible = false;
		//		PopulateSummaryLine(GroupControl1.GroupByMonth(), DVGroupByMonth, "Month");
               
		//	}
		//	if (GroupControl1.SelectedCategory.Equals("Year"))
		//	{
		//		SearchFilterCore.GroupBy = "Year";
		//		DVGroupByYear.DataSource = GroupControl1.GroupByYear();
		//		DVGroupByYear.DataBind();
		//		DVGroupByDay.Visible = false;
		//		DVGroupByMonth.Visible = false;
		//		DVGroupByYear.Visible = true;
		//		summary.Visible = false;
		//		PopulateSummaryLine(GroupControl1.GroupByYear(), DVGroupByYear, "Year");
                
		//	}
		//	oSearchFilter.RaiseSearch();
		//	SearchFilterCore.RaiseSearch();

		//}

		//protected void DVGroupByDay_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
		//{

		//	DVGroupByDay.PageIndex = e.NewPageIndex;
		//	DVGroupByDay.DataSource = GroupControl1.GroupByDay();
		//	DVGroupByDay.DataBind();
		//	PopulateSummaryLine(GroupControl1.GroupByDay(), DVGroupByDay, "Day");

		//}

		//protected void DVGroupByYear_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
		//{

		//	DVGroupByYear.PageIndex = e.NewPageIndex;
		//	DVGroupByYear.DataSource = GroupControl1.GroupByYear();
		//	DVGroupByYear.DataBind();
		//	PopulateSummaryLine(GroupControl1.GroupByYear(), DVGroupByYear, "Year");
		//}

		//private void PopulateSummaryLine(DataTable dt, DetailsView dv, string category)
		//{
		//	Label keylbl = (Label)dv.FindControl("lbl1");
		//	var key = keylbl.Text;

		//	for (var i = 0; i < dt.Rows.Count; i++)
		//	{
		//		if (dt.Rows[i].ItemArray[0].ToString().Equals(key))
		//		{
		//			lblSummaryLine.Text = "Summary of " + category + ": " + key + " - Total hours worked: " + dt.Rows[i].ItemArray[1].ToString() +
		//					   " ,Average Start Time: " + dt.Rows[i].ItemArray[2].ToString() +
		//					   " ,Average End Time: " + dt.Rows[i].ItemArray[3].ToString();
		//			break;

		//		}
		//	}
		//}
		//protected void DVGroupByMonth_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
		//{

		//	DVGroupByMonth.PageIndex = e.NewPageIndex;
		//	DVGroupByMonth.DataSource = GroupControl1.GroupByMonth();
		//	DVGroupByMonth.DataBind();
		//	PopulateSummaryLine(GroupControl1.GroupByMonth(), DVGroupByMonth, "Month");

		//}


		#region Events		

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.Schedule;
			PrimaryEntityKey = "Schedule";
			PrimaryEntityIdColumn = "ScheduleId";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);

			GroupListCore = oGroupList;

			//SearchFilterCore.GroupBy = "Person";

			IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
			//GroupControl1.OnGroup += oSearchFilter_OnGroup;
		}	

		#endregion

    }
}