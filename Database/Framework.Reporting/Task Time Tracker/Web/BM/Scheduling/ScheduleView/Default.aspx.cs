using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Collections;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;
using ApplicationContainer.UI.Web.Scheduling.Schedule.Controls;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleView
{
	public partial class Default : BasePage
	{

		#region variable

		DataTable AllDataRows;
		
		int AlternateCount = 0;

		//int		ReleaseLogId;
		int TotalParentCount;
		//string	ReleaseLog;

		int ItemCount;

		List<decimal> LstMedians = new List<decimal>();
		List<decimal> LstTotal = new List<decimal>();
		List<decimal> LstCount = new List<decimal>();
		List<decimal> LstAverage = new List<decimal>();
		List<decimal> LstMax = new List<decimal>();
		List<decimal> LstMin = new List<decimal>();		

		protected ControlVisibilityManager VisibilityManagerCore { get; set; }

		private int DetailUserPreferenceCategoryId
		{
			get
			{
				return Convert.ToInt32(ViewState["DetailUserPreferenceCategoryId"]);
			}
			set
			{
				ViewState["DetailUserPreferenceCategoryId"] = value;
			}
		}

		decimal TotalTimeSpentCount
		{
			get
			{
				return Convert.ToDecimal(ViewState["TotalTimeSpent"]);
			}
			set
			{
				ViewState["TotalTimeSpent"] = value;
			}
		}

		#endregion

		#region HTML Methods

		public string StyleStat()
		{
			AlternateCount++;

			var style = string.Empty;

			if (AlternateCount % 2 == 0)

				style = "rowRN";
			else
				style = "rowAlternate";

			return style;
		}

		public HtmlGenericControl GetStatisticInfoHTMLHeaderRow(string headerName)
		{
			var row = new HtmlGenericControl("div");
            row.Attributes["class"] = "row gutter-border text-center";

			var cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-2";
			cell.InnerText = headerName;
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Total";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-2";
			cell.InnerText = "Total(%)";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Average";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Median";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Count";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-2";
			cell.InnerText = "Count(%)";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Max";
			row.Controls.Add(cell);

			cell = new HtmlGenericControl("div");
			cell.Attributes["class"] = "col-sm-1";
			cell.InnerText = "Min";
			
			row.Controls.Add(cell);
			return row;
		}

		public HtmlGenericControl GetStatisticInfoHTMLSummaryRow(DataTable scheduleData, string groupName)
		{
			var lstValues = ScheduleDataManager.GetStatisticDataSummary(scheduleData, ScheduleDataModel.DataColumns.ScheduleTimeSpentConstant, ApplicationCommon.ScheduleStatisticUnknown);
			var average = Math.Round(lstValues["Average"], 2);
			var median = lstValues["Median"];

			var recCount = scheduleData.Rows.Count;
			decimal total = 0;

			decimal maxValue = 0;
			decimal minValue = 0;

			// if no records, no need to count total, search for max, min
			if (recCount > 0)
			{
				var series = new decimal[recCount];
				var i = 0;

				foreach (var itemSchedule in scheduleData.AsEnumerable())
				{
					var totalHoursWorked = itemSchedule[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString();

					var totalHoursWorkedValue = 0m;

					if (!Decimal.TryParse(totalHoursWorked, out totalHoursWorkedValue))
					{
						totalHoursWorkedValue = ScheduleDataModel.DataColumns.ScheduleTimeSpentConstant;
					}

					series[i++] = totalHoursWorkedValue;
				}

				total = series.Sum(x => x);

				maxValue = series.Select(x => x).Distinct().Max();
				minValue = series.Select(x => x).Distinct().Min();
			}


			var subDiv1 = new HtmlGenericControl("div");
            subDiv1.Attributes["class"] = "row gutter-border text-right";

			var divId1 = new HtmlGenericControl("div");
            divId1.Attributes["class"] = "col-sm-2";

			var lblScheduleText1 = new Label();
			lblScheduleText1.Text = "Summary";
			divId1.Controls.Add(lblScheduleText1);
			subDiv1.Controls.Add(divId1);

			var divTotal1 = new HtmlGenericControl("div");
            divTotal1.Attributes["class"] = "col-sm-1";

			var lblTotalText1 = new Label();
			lblTotalText1.Text = total.ToString("#0,0.00");
			divTotal1.Controls.Add(lblTotalText1);
			subDiv1.Controls.Add(divTotal1);

			var divTotalPer = new HtmlGenericControl("div");
            divTotalPer.Attributes["class"] = "col-sm-2";

			var lblTotalPerText = new Label();
			lblTotalPerText.Text = "100%";
			divTotalPer.Controls.Add(lblTotalPerText);
			subDiv1.Controls.Add(divTotalPer);

			var divAverage1 = new HtmlGenericControl("div");
            divAverage1.Attributes["class"] = "col-sm-1";

			var lblAverageText1 = new Label();
			lblAverageText1.Text = average.ToString("#0,0.00");
			divAverage1.Controls.Add(lblAverageText1);
			subDiv1.Controls.Add(divAverage1);

			var divMedianSummary = new HtmlGenericControl("div");
            divMedianSummary.Attributes["class"] = "col-sm-1";

			var lblMedianText1 = new Label();
			lblMedianText1.Text = median.ToString("#0,0.00");
			divMedianSummary.Controls.Add(lblMedianText1);
			subDiv1.Controls.Add(divMedianSummary);

			var divCountSummary = new HtmlGenericControl("div");
            divCountSummary.Attributes["class"] = "col-sm-1";

			var lblCountText1 = new Label();
			lblCountText1.Text = recCount.ToString("#0,0.00");
			divCountSummary.Controls.Add(lblCountText1);
			subDiv1.Controls.Add(divCountSummary);

			var divCntPer = new HtmlGenericControl("div");
            divCntPer.Attributes["class"] = "col-sm-2";

			var lblCntPerText = new Label();
			lblCntPerText.Text = "100%";
			divCntPer.Controls.Add(lblCntPerText);
			subDiv1.Controls.Add(divCntPer);

			var divMaxSummary = new HtmlGenericControl("div");
            divMaxSummary.Attributes["class"] = "col-sm-1";

			var lblMaxText1 = new Label();
			lblMaxText1.Text = maxValue.ToString("#0,0.00");
			divMaxSummary.Controls.Add(lblMaxText1);
			subDiv1.Controls.Add(divMaxSummary);

			var divMinSummary = new HtmlGenericControl("div");
            divMinSummary.Attributes["class"] = "col-sm-1";

			var lblMinText1 = new Label();
			lblMinText1.Text = minValue.ToString("#0,0.00");
			divMinSummary.Controls.Add(lblMinText1);
			
			subDiv1.Controls.Add(divMinSummary);
			return subDiv1;
		}

		public HtmlGenericControl GetStatisticInfoHTMLRow(Statistic item, string key, decimal totalHoursCount = 0, decimal totalRowCount = 0)
		{
			var subDiv = new HtmlGenericControl("div");
            subDiv.Attributes["class"] = "row gutter-border text-right";

			var divId = new HtmlGenericControl("div");
			divId.Attributes["class"] = "col-sm-2";

			var lblScheduleText = new Label();
			lblScheduleText.Text = key;
			divId.Controls.Add(lblScheduleText);
			subDiv.Controls.Add(divId);

			var divTotal = new HtmlGenericControl("div");
			divTotal.Attributes["class"] = "col-sm-1";
			var lblTotalText = new Label();
			lblTotalText.Text = ((decimal)item.Total).ToString("#0,0.00");

			divTotal.Controls.Add(lblTotalText);
			subDiv.Controls.Add(divTotal);

			var divTotalPercentage = new HtmlGenericControl("div");
			divTotalPercentage.Attributes["class"] = "col-sm-2";

			if (totalHoursCount != 0)
			{
				item.TotalPercentage = (item.Total / totalHoursCount) * 100;

				var lblTotalPercentageText = new Label();
				lblTotalPercentageText.Text += item.TotalPercentage.Value.ToString("#0.00") + "%";

				divTotalPercentage.Controls.Add(lblTotalPercentageText);
			}
			subDiv.Controls.Add(divTotalPercentage);

			var divAverage = new HtmlGenericControl("div");
			divAverage.Attributes["class"] = "col-sm-1";

			var lblAverageText = new Label();
			if (item.Average != null)
				lblAverageText.Text = Math.Round(Convert.ToDecimal(item.Average), 2).ToString("#0,0.00");
			divAverage.Controls.Add(lblAverageText);
			subDiv.Controls.Add(divAverage);

			var divMedian = new HtmlGenericControl("div");
			divMedian.Attributes["class"] = "col-sm-1";

			var lblMedianText = new Label();
			if (item.Median != null)
				lblMedianText.Text = ((decimal)item.Median).ToString("#0,0.00");
			divMedian.Controls.Add(lblMedianText);
			subDiv.Controls.Add(divMedian);

			var divCount = new HtmlGenericControl("div");
			divCount.Attributes["class"] = "col-sm-1";
			var lblCountText = new Label();
			lblCountText.Text = ((decimal)item.Count).ToString("#0,0.00");
			divCount.Controls.Add(lblCountText);
			subDiv.Controls.Add(divCount);

			var divCountPercentage = new HtmlGenericControl("div");
			divCountPercentage.Attributes["class"] = "col-sm-2";

			if (totalRowCount != 0)
			{
				item.CountPercentage = (item.Count / totalRowCount) * 100;

				var lblCountPercentageText = new Label();
				lblCountPercentageText.Text += item.CountPercentage.Value.ToString("#0.00") + "%";

				divCountPercentage.Controls.Add(lblCountPercentageText);
			}
			subDiv.Controls.Add(divCountPercentage);

			var divMax = new HtmlGenericControl("div");
			divMax.Attributes["class"] = "col-sm-1";

			var lblMaxText = new Label();
			if (item.Max != null)
				lblMaxText.Text = ((decimal)item.Max).ToString("#0,0.00");
			divMax.Controls.Add(lblMaxText);
			subDiv.Controls.Add(divMax);

			var divMin = new HtmlGenericControl("div");
			divMin.Attributes["class"] = "col-sm-1";

			var lblMinText = new Label();
			if (item.Min != null)
				lblMinText.Text = ((decimal)item.Min).ToString("#0,0.00");
			divMin.Controls.Add(lblMinText);
			
			subDiv.Controls.Add(divMin);
			return subDiv;

		}

		#endregion

		#region Data Methods

		protected DataTable GetData()
		{
            var ds = ScheduleDataManager.SearchView(oSearchFilter.SearchParameters,SessionVariables.RequestProfile);

			AllDataRows = ds.Tables[1];
			DataTable dt = null;

			// refresh total time spent variable everytime new data is retrieved.
			TotalTimeSpentCount = GetTotalTimeSpentCount();

			if (string.IsNullOrEmpty(oSearchFilter.GroupBy) || oSearchFilter.GroupBy == "-1" || oSearchFilter.GroupBy == "All")
			{
				if (ds.Tables.Count > 1)
				{
					var tblKeyDescription = new DataTable();

					tblKeyDescription.AcceptChanges();
					tblKeyDescription.Columns.Add("Person");
					tblKeyDescription.Columns.Add("PersonId");

					var row = tblKeyDescription.NewRow();
					row["Person"] = "All";
					row["PersonId"] = "All";
					tblKeyDescription.Rows.Add(row);

					var dataView = tblKeyDescription.DefaultView;

					dt = dataView.ToTable();
					GrdParentGrid.DataSource = dt;
					GrdParentGrid.DataBind();

					TotalParentCount = dt.Rows.Count;
				}
			}
			//if (GroupBy.Equals("WorkDate–Day") || SubGroupBy.Equals("WorkDate–Day"))
			//{
			//	dt.Columns.Add("WorkDate–Day", typeof(System.Int32));

			//	foreach (DataRow dr in dt.Rows)
			//	{
			//		var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
			//		dr["WorkDate–Day"] = workdate.Day;
			//	}
			//}
			//else if (GroupBy.Equals("WorkDate–Month") || SubGroupBy.Equals("WorkDate–Month"))
			//{
			//	dt.Columns.Add("WorkDate–Month", typeof(System.String));

			//	foreach (DataRow dr in dt.Rows)
			//	{
			//		var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
			//		var mfi = new System.Globalization.DateTimeFormatInfo();
			//		var strMonthName = mfi.GetAbbreviatedMonthName(workdate.Month).ToString();
			//		dr["WorkDate–Month"] = strMonthName;
			//	}
			//}
			//else if (GroupBy.Equals("WorkDate–Year") || SubGroupBy.Equals("WorkDate–Year"))
			//{
			//	dt.Columns.Add("WorkDate–Year", typeof(System.Int32));

			//	foreach (DataRow dr in dt.Rows)
			//	{
			//		var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
			//		dr["WorkDate–Year"] = workdate.Year;
			//	}
			//}
			//else if (GroupBy.Equals("WorkDate–Week") || SubGroupBy.Equals("WorkDate–Week"))
			//{
			//	dt.Columns.Add("WorkDate–Week", typeof(System.Int32));

			//	foreach (DataRow dr in dt.Rows)
			//	{
			//		var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
			//		dr["WorkDate–Week"] = workdate.DayOfWeek;
			//	}
			//}
			//else if (GroupBy.Equals("WorkDate–Quarter") || SubGroupBy.Equals("WorkDate–Quarter"))
			//{
			//	dt.Columns.Add("WorkDate–Quarter", typeof(System.String));

			//	foreach (DataRow dr in dt.Rows)
			//	{
			//		var workdate = (DateTime)dr[ScheduleDataModel.DataColumns.WorkDate];
			//		var quarter = ((workdate.Month - 3) % 12) / 4;

			//		switch (quarter)
			//		{
			//			case 1:
			//				dr["WorkDate–Quarter"] = "Q1";
			//				break;
			//			case 2:
			//				dr["WorkDate–Quarter"] = "Q2";
			//				break;
			//			case 3:
			//				dr["WorkDate–Quarter"] = "Q3";
			//				break;
			//			case 4:
			//				dr["WorkDate–Quarter"] = "Q4";
			//				break;
			//		}					 
			//	}
			//}

			//dt.AcceptChanges();
			else if (ds.Tables.Count > 1 && !String.IsNullOrEmpty(oSearchFilter.GroupBy.Trim()) && oSearchFilter.GroupBy != "-1" && oSearchFilter.GroupBy != "All")
			{

				// adding statistic header row only if only Grouping is applied.
				if (String.IsNullOrEmpty(oSearchFilter.SubGroupBy) || oSearchFilter.SubGroupBy == "-1" || oSearchFilter.SubGroupBy == "All")
				{
					TableReportContent.Controls.Add(GetStatisticInfoHTMLHeaderRow(oSearchFilter.GroupBy));
				}
				// All related fings (children)

				var AllDataRows_Schedule = ds.Tables[0];

				var tblKeyDescription = new DataTable();
				tblKeyDescription.Columns.Add("Grouping");
				tblKeyDescription.Columns.Add("Key");

				// temp
				tblKeyDescription.Columns.Add("PersonId");
				tblKeyDescription.Columns.Add("Person");


				tblKeyDescription.AcceptChanges();

				AllDataRows_Schedule.Merge(AllDataRows);
				var distinctFieldValues = (from row in AllDataRows_Schedule.AsEnumerable()
										  .Where(row => row[oSearchFilter.GroupBy].ToString().Trim() != "")
										   orderby row[oSearchFilter.GroupBy].ToString().Trim() descending
										   select row[oSearchFilter.GroupBy].ToString().Trim())
										   .Distinct(StringComparer.CurrentCultureIgnoreCase);

				//var distinctFieldValues = (from row in AllDataRows.AsEnumerable()
				//						   orderby row["PersonId"].ToString().Trim()
				//						   select row["PersonId"].ToString().Trim()).Distinct(StringComparer.CurrentCultureIgnoreCase);

				foreach (var key in distinctFieldValues)
				{
					var row = tblKeyDescription.NewRow();
					row["Grouping"] = oSearchFilter.GroupBy;
					row["Key"] = key;

					// temp
					//row["PersonId"] = key;
					//row["Person"] = key;

					EnumerableRowCollection<object> schedulePKId = null;

					//if (AllDataRows_Schedule.Columns.Contains("IsUpdated"))
					//{
					//	schedulePKId = AllDataRows_Schedule.AsEnumerable()
					//						.Where(x => x[ScheduleDataModel.DataColumns.Person].ToString().ToLower() == key &&
					//						 x[ScheduleDataModel.DataColumns.IsUpdated].ToString() == "1")
					//						.Select(x => x[ScheduleDataModel.DataColumns.PersonId]);
					//}
					//else
					//{

						schedulePKId = AllDataRows_Schedule.AsEnumerable()
											 .Where(x => x[ScheduleDataModel.DataColumns.Person].ToString().ToLower() == key)
												 .Select(x => x[ScheduleDetailDataModel.DataColumns.PersonId]);
					//}

					//if (oSearchFilter.GroupBy == ScheduleDataModel.DataColumns.Person)
					 //{
						 if (schedulePKId.Count() != 0)
						 {
							 var rowItem = from rowPK in AllDataRows_Schedule.AsEnumerable() select rowPK;
							 row["PersonId"] = rowItem.First();
							 row["Person"] = key;
							 tblKeyDescription.Rows.Add(row);
						 }
					 //}
					else
					{
						row["Person"] = key;
						tblKeyDescription.Rows.Add(row);
					}
					//row["ReleaseLogId"] = key;

					//row["Person"] = key;

					//tblKeyDescription.Rows.Add(row);
				}

				tblKeyDescription.AcceptChanges();

				var dataView = tblKeyDescription.DefaultView;// ds.Tables[0].DefaultView;				
				//dataView.Sort = ScheduleDataModel.DataColumns.PersonId + " DESC";

				dt = dataView.ToTable();

				TotalParentCount = dt.Rows.Count;

				GrdParentGrid.DataSource = dt;
				GrdParentGrid.DataBind();
			}

			//TotalParentCount = dt.Rows.Count;

			if ((ds.Tables[1].Rows.Count == 0 && ds.Tables[0].Rows.Count < 1) || (ds.Tables[1].Rows.Count == 0 && ds.Tables[0].Rows.Count > 1))
			{
				lblSearchStatus.Text = "No records found for the given Search parameters.";
				//StatsDiv.Visible = false;
			}
			else
			{
				lblSearchStatus.Text = String.Empty;
				//StatsDiv.Visible = true;
			}

			return ds.Tables[0];
		}

		private DataTable GetSubGroupData(string groupKey, string subGroupKey)
		{
			DataTable dt = null;

			var groupOn = oSearchFilter.GroupBy;
			var subGroupOn = oSearchFilter.SubGroupBy;

			// this should not happen ...
			if (string.IsNullOrEmpty(groupOn) || groupOn == "-1" || string.IsNullOrEmpty(subGroupOn) || subGroupOn == "-1")
			{
				//groupOn = ScheduleDataModel.DataColumns.ScheduleId;
				groupOn = ScheduleDataModel.DataColumns.PersonId;
				return AllDataRows;
			}

			if (AllDataRows != null)
			{
				var dataView = AllDataRows.DefaultView;
				dataView.RowFilter = groupOn + " = '" + groupKey + "' and " + subGroupOn + " = '" + subGroupKey + "'";

				dt = dataView.ToTable();
			}

			return dt;
		}

		private DataTable GetDataDetailsByKey(string key)
		{
			DataTable dt = null;

			var groupOn = oSearchFilter.GroupBy;

			// this should not happen ...
			if (string.IsNullOrEmpty(groupOn) || groupOn == "-1")
			{
				groupOn = ScheduleDataModel.DataColumns.Person;

				return AllDataRows;
			}

			if (AllDataRows != null)
			{
				var dataView = AllDataRows.DefaultView;
				dataView.RowFilter = groupOn + " = '" + key + "'";

				dt = dataView.ToTable();
			}

			return dt;
		}

		#endregion

		#region Methods

		private decimal GetTotalTimeSpentCount()
		{
			var StatisticUnknown = "unknown";

			// totalTimeSpent1 calculates the time of 'UnKnown' value based on the ReleaseNotesTimeSpentConstant
			decimal totalTimeSpent1 = AllDataRows.AsEnumerable()
										 .Where(z => (string.IsNullOrEmpty(z[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString()) || z[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == StatisticUnknown))
										 .Count() * ScheduleDataModel.DataColumns.ScheduleTimeSpentConstant;

			// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
			var totalTimeSpent2 = AllDataRows.AsEnumerable()
										 .Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != ApplicationCommon.ScheduleStatisticUnknown)
										 .Sum(x => Convert.ToDecimal(x[ScheduleDataModel.DataColumns.TotalHoursWorked]));

			return totalTimeSpent1 + totalTimeSpent2;
		}		

		private string[] GetColumns()
		{
			if (!ddlFieldConfigurationMode.SelectedItem.Text.Equals(String.Empty))
				return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Schedule, ddlFieldConfigurationMode.SelectedValue, SessionVariables.RequestProfile);
			else
				return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Schedule, "DBColumns", SessionVariables.RequestProfile);
		}

		private void AddSummaryStatisticLine(string parentKey, Statistic item)
		{
			// why would this happen?
			if (item.Count == 0) return;

			LstMedians.Add((decimal)item.Median);
			LstTotal.Add((decimal)item.Total);
			LstAverage.Add((decimal)item.Average);
			LstCount.Add((decimal)item.Count);
			LstMax.Add((decimal)item.Max);
			LstMin.Add((decimal)item.Min);

			ItemCount = GrdParentGrid.Items.Count;
		}

		private void AddSummaryStatisticLine(RepeaterItemEventArgs e, string parentKey, Statistic item)
		{
			// why would this happen?
			if (item.Count == 0) return;

			LstMedians.Add((decimal)item.Median);
			LstTotal.Add((decimal)item.Total);
			LstAverage.Add((decimal)item.Average);
			LstCount.Add((decimal)item.Count);
			LstMax.Add((decimal)item.Max);
			LstMin.Add((decimal)item.Min);

			var lblCount = e.Item.FindControl("lblCount") as Label;
			lblCount.Text = item.Total.ToString();

			ItemCount = GrdParentGrid.Items.Count;

			var lblAverage = e.Item.FindControl("lblAverageValue") as Label;
			lblAverage.Text = Math.Round(Convert.ToDecimal(item.Average), 2).ToString();

			var lblMedian = e.Item.FindControl("lblMedianValue") as Label;
			lblMedian.Text = item.Median.ToString();

			var lblRecordCount = e.Item.FindControl("lblRecordCount") as Label;
			lblRecordCount.Text = item.Count.ToString();

			var lblMax = e.Item.FindControl("lblMax") as Label;
			lblMax.Text = item.Max.ToString();

			var lblMin = e.Item.FindControl("lblMin") as Label;
			lblMin.Text = item.Min.ToString();
		}

		protected void chkbox_Click(Object sender, EventArgs e)
		{
			if (sender == null) return;
			
			// TODO: why do we have try catch
			try
			{
				var scheduleid = ((LinkButton)sender).CommandName;
				for (var i = 0; i < GrdParentGrid.Items.Count; i++)
				{
					var hdnfield = (HiddenField)GrdParentGrid.Items[i].FindControl("hdnScheduleId");
					if (hdnfield != null)
					{
						if (hdnfield.Value.Equals(scheduleid))
						{
							GrdParentGrid.Items[i].Visible = false;
						}
					}
				}
			}
			catch (Exception ex) { }
			
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

		protected void GetTabControl()
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.Setup("ScheduleView");

			var selected = false;

			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}

			var divGrid = new HtmlGenericControl("div");
			divGrid.Attributes.Add("style", "padding:30px;");

			divGrid.Controls.Add(plcGroupByHolder);
			divGrid.Controls.Add(GrdParentGrid);

			tabControl.AddTab("Schedule", divGrid, String.Empty, selected);

			selected = false;

			if (Request.QueryString["tab"] == "2")
			{
				selected = true;
			}

			// Statistic Info
			var subDiv = new HtmlGenericControl("div");
			subDiv.Attributes.Add("style", "padding:30px;");
			subDiv.Controls.Add(TableReportContent);

			tabControl.AddTab("Statistic Info", subDiv);

			//// Charts & Graphs
			var divGraph = new HtmlGenericControl("div");
			divGraph.Attributes.Add("style", "padding:30px;");
			divGraph.Controls.Add(dynChart);

			tabControl.AddTab("Charts & Graphs", divGraph);

			plcTabHolder.Controls.Add(tabControl);

		}

		private void FormatListControl(bool addStyle)
		{
			if (addStyle)
			{
				for (var i = 0; i < GrdParentGrid.Items.Count; i++)
				{
					var list = (DetailsWithChildrenControl)GrdParentGrid.Items[i].FindControl("oList");

					if (list != null)
					{

						//list.MainGridViewList.RowStyle.Height = 10;
						//list.MainGridViewList.AlternatingRowStyle.Height = 10;
						//list.MainGridViewList.GridLines = GridLines.None;
						//list.MainGridViewList.RowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#696969");
						//list.MainGridViewList.AlternatingRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#696969");
						//list.MainGridViewList.RowStyle.Font.Size = 8;
						//list.MainGridViewList.AlternatingRowStyle.Font.Size = 8;
						//list.MainGridViewList.HeaderStyle.Height = 10;
						//list.MainGridViewList.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#3A4F63");
						//list.MainGridViewList.HeaderStyle.ForeColor = System.Drawing.Color.White;
						//list.MainGridViewList.HeaderStyle.Height = 10;
						//list.MainGridViewList.HeaderStyle.Font.Size = 9;
						//list.MainGridViewList.GridLines = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesGridLines);

						//list.MainGridViewList.RowStyle.Height = new Unit(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleHeight));
						//list.MainGridViewList.AlternatingRowStyle.Height = new Unit(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight));
						//list.MainGridViewList.RowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleForeColor));
						//list.MainGridViewList.AlternatingRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor));
						//list.MainGridViewList.RowStyle.Font.Size = new FontUnit(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleFontSize));
						//list.MainGridViewList.AlternatingRowStyle.Font.Size = new FontUnit(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize));
						//list.MainGridViewList.HeaderStyle.Height = new Unit(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleHeight));
						//list.MainGridViewList.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderBackColor));
						//list.MainGridViewList.HeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderForeColor));
						//list.MainGridViewList.HeaderStyle.Font.Size = new FontUnit(PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleFontSize));
						
						//string value = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ScheduleNewGridLines);
						var value = "None";

						switch (value)
						{
							case "Both":
								list.MainGridViewList.GridLines = GridLines.Both;
								break;

							case "None":
								list.MainGridViewList.GridLines = GridLines.None;
								break;

							case "Horizontal":
								list.MainGridViewList.GridLines = GridLines.Horizontal;
								break;

							case "Vertical":
								list.MainGridViewList.GridLines = GridLines.Vertical;
								break;
						}

					}
				}
			}
			else
			{
				for (var i = 0; i < GrdParentGrid.Items.Count; i++)
				{
					var list = (DetailsWithChildrenControl)GrdParentGrid.Items[i].FindControl("oList");

					if (list != null)
					{
						for (var j = 0; j < list.MainGridViewList.Rows.Count; j++)
						{
							list.MainGridViewList.Rows[j].Style.Clear();
							list.MainGridViewList.GridLines = GridLines.Both;
						}

					}
				}
			}
		}

		private void ManageControlVisibility(string controlTitle)
		{
			var sbm = Master.SubMenuObject;

			switch (controlTitle)
			{
				case "Search Box":
					oSearchFilter.Visible = true;
					PerferenceUtility.UpdateUserPreference(oSearchFilter.SearchControl.SettingCategory, ApplicationCommon.ControlVisible, "true");
					break;

				case "Sub Menu":
					sbm.Visible = true;
					PerferenceUtility.UpdateUserPreference(sbm.SettingCategory, ApplicationCommon.ControlVisible, "true");
					break;
			}
		}

		protected void ClearValues()
		{
			TableReportContent.Controls.Clear();

			//TableReportContent.Controls.Add(StatsDiv);

			plcGroupByHolder.Controls.Clear();

			LstTotal.Clear();
			LstAverage.Clear();
			LstMax.Clear();
			LstMin.Clear();
			LstCount.Clear();
			LstMedians.Clear();
		}

		private DataTable GetApplicableModesList(int systemEntityTypeId)
		{
			var data = new FieldConfigurationDataModel();
			data.SystemEntityTypeId = systemEntityTypeId;

			var columns = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);
			var modes = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);

			var validModes = new DataTable();
			validModes = modes.Clone();

			for (var j = 0; j < modes.Rows.Count; j++)
			{
				for (var i = 0; i < columns.Rows.Count; i++)
				{

					if (
						int.Parse(
							columns.Rows[i][
								FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId].
								ToString()) ==
						int.Parse(
							modes.Rows[j][
								FieldConfigurationModeDataModel.DataColumns.
									FieldConfigurationModeId].ToString())
						)
					{
						var temp = validModes.Select("FieldConfigurationModeId = " + int.Parse(
							modes.Rows[j][
								FieldConfigurationModeDataModel.DataColumns.
									FieldConfigurationModeId].ToString()));
						if (temp.Length == 0)
							validModes.ImportRow(modes.Rows[j]);


					}
				}
			}

			var dv = validModes.DefaultView;
			dv.Sort = "SortOrder ASC";

			var sortedValidModes = dv.ToTable();
			return sortedValidModes;
		}

		private int SetUpDropDownFCMode()
		{
			var systemEntityTypeId	= (int)Enum.Parse(typeof(SystemEntity), "Schedule");
			var dt					= GetApplicableModesList(systemEntityTypeId);
			var modeSelected		= SessionVariables.GetSessionInstanceFCMode("Schedule");
			var settingCategory		= "ScheduleNewDefaultViewListControl";
			var upcId				= PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(settingCategory, settingCategory);
			var fcModeSelected		= PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.FieldConfigurationMode, settingCategory);
			var currentEntity		= "Schedule";

			if (dt.Rows.Count > 0)
			{
				ddlFieldConfigurationMode.DataSource = dt;
				ddlFieldConfigurationMode.DataTextField = "Name";
				ddlFieldConfigurationMode.DataValueField = "FieldConfigurationModeId";
				ddlFieldConfigurationMode.DataBind();
				ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString();

				if (Convert.ToInt32(fcModeSelected) > 0)
				{
					ddlFieldConfigurationMode.SelectedValue = fcModeSelected.ToString();
					SessionVariables.SaveSessionInstanceFCMode(Convert.ToInt32(fcModeSelected), currentEntity);
					modeSelected = (int)Session["Schedule" + "SelectedMode"];
				}
				else
					ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString();
			}
			else
			{
				ddlFieldConfigurationMode.Visible = false;
			}

			return modeSelected;
		}

		#endregion

		#region Page Events

		protected override void OnInit(EventArgs e)
		{
			SettingCategory                             = "ScheduleNewDefaultView";
			DetailUserPreferenceCategoryId              = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists("Schedule", "Schedule");
			VisibilityManagerCore                       = oVC;
			oSearchFilter.SearchControl.SettingCategory = SettingCategory + "SearchControl";

			oSearchFilter.GetFilter(SystemEntity.Schedule, "ScheduleId");
			GetTabControl(); 
		}		

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var sbm = Master.SubMenuObject;

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			var bcControl = Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup(string.Empty);
			bcControl.GenerateMenu();

			VisibilityManagerCore = oVC;

			var isSubMenuVisible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, sbm.SettingCategory);
			var isSearchControlVisible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, oSearchFilter.SearchControl.SettingCategory);

			// set visibility
			oSearchFilter.Visible = isSearchControlVisible;
			sbm.Visible = isSubMenuVisible;

			VisibilityManagerCore.ClearChildMenuItems();

			VisibilityManagerCore.AddChildControl(oSearchFilter.SearchControl.Title, isSearchControlVisible);
			VisibilityManagerCore.AddChildControl(sbm.Title, isSubMenuVisible);

			// bccontrol.SettingCategory = SettingCategory + "BreadCrumbControl";
			// bccontrol.Setup("");
			// bccontrol.GenerateMenu();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			var sbm = Master.SubMenuObject;

			if (!IsPostBack)
			{
				oSearchFilter.SearchControl.Title = "Search Box";
				sbm.Title = "Sub Menu";
				SetUpDropDownFCMode();
			}

			VisibilityManagerCore.Setup(ManageControlVisibility, SettingCategory);

			oSearchFilter.SearchControl.SetupSearch();

			GetData();

			//TabSetUp();

			FormatListControl(true);

			//lnkGridStyle.Text = "Classic";

			//Log4Net.LogInfo("ReleaseNotes Page Load","PageLoad",100);
			oSearchFilter.SearchControl.OnSearch += oSearchFilter_OnSearch;
			//Log4Net.LogInfo("End ReleaseNotes Page Load", "PageLoad", 100);
		}

		#endregion

		#region Control Events

		void oSearchFilter_OnSearch(object sender, EventArgs e)
		{
			ClearValues();
			GetData();
		}

		protected void ddlFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			var currentEntity = "Schedule";
			GetData();

			var fcMode = ddlFieldConfigurationMode.SelectedValue;
			SessionVariables.SaveSessionInstanceFCMode(Convert.ToInt32(fcMode), currentEntity);

			var settingCategory = "ScheduleNewDefaultViewListControl";
			PerferenceUtility.UpdateUserPreference(settingCategory, ApplicationCommon.FieldConfigurationMode, fcMode);
		}

		#endregion

		#region Grid Events

		protected void GrdParentGrid_RowDataBound(object sender, RepeaterItemEventArgs e)
		{
			var drv = e.Item.DataItem as DataRowView;

			if (drv == null) return;

			var hdnField = e.Item.FindControl("hdnScheduleId") as HiddenField;
			var btnInsert = e.Item.FindControl("btnInsert") as LinkButton;
			var btnCheckBox = e.Item.FindControl("chkbox") as LinkButton;

			//tab control or detail Grid Container DIV
			var detailsGridContainer = e.Item.FindControl("detailsGridContainer") as HtmlGenericControl;

			if (oSearchFilter.GroupBy == "Person")
			{
				//var btnInsert = e.Item.FindControl("btnInsert") as LinkButton;
				btnInsert.PostBackUrl = Page.GetRouteUrl("ScheduleEntityRoute", new { Action = "Insert" });
				btnInsert.PostBackUrl += "/" + hdnField.Value;
			}

			var parentKey = string.Empty;
			var parentKeyName = parentKey = DataBinder.Eval(e.Item.DataItem, "Person").ToString();

			//ReleaseLog = (DataBinder.Eval(e.Item.DataItem, "Name")).ToString();
			//ReleaseLog = parentKey;

			if (oSearchFilter.GroupBy == "-1" || string.IsNullOrEmpty(oSearchFilter.GroupBy) || oSearchFilter.GroupBy == "All")
			{
				parentKey = hdnField.Value;
				btnInsert.Visible = false;
				//btnCheckBox.Visible = false;
			}
			else
			{
				parentKey = DataBinder.Eval(e.Item.DataItem, "Key").ToString();
			}

			//data set for the current group
			var filteredGroup = GetDataDetailsByKey(parentKey);

			var statisticItem = ScheduleDataManager.GetStatisticData(filteredGroup, ScheduleDataModel.DataColumns.ScheduleTimeSpentConstant, ApplicationCommon.ScheduleStatisticUnknown);

			AddSummaryStatisticLine(parentKey, statisticItem);			

			if(!String.IsNullOrEmpty(oSearchFilter.GroupBy.Trim())
				&&	oSearchFilter.GroupBy != "-1"
				&&	oSearchFilter.GroupBy != "All"
				&&	!String.IsNullOrEmpty(oSearchFilter.SubGroupBy)
				&&	oSearchFilter.SubGroupBy != "-1"
				&&	oSearchFilter.SubGroupBy != "All")
				//&&	filteredGroup.Columns.Contains(oSearchFilter.SubGroupBy)			
			{

				// get new instance of tab control to added for the group
				var tabControl = ApplicationCommon.GetNewDetailTabControl();
				tabControl.Setup(SettingCategory);

				var subGroupByColumn = oSearchFilter.SubGroupBy;

				// Add Group By Key Information
				var grpByDiv = new HtmlGenericControl("div");
                grpByDiv.InnerHtml = "<strong>Group: " + parentKey +" </strong>";
				TableReportContent.Controls.Add(grpByDiv);

				// Add Header Row for Statistics table
				TableReportContent.Controls.Add(GetStatisticInfoHTMLHeaderRow(subGroupByColumn));

				// get distinct sub group by values
				var distinctFieldValues = (from row in filteredGroup.AsEnumerable()
										   .Where(row => row[subGroupByColumn].ToString().Trim() != "")
										   orderby row[subGroupByColumn].ToString().Trim() descending
										   select row[subGroupByColumn].ToString().Trim())
										   .Distinct(StringComparer.CurrentCultureIgnoreCase);

				var series = new decimal[filteredGroup.Rows.Count];
				var i = 0;

				foreach (DataRow item in filteredGroup.Rows)
				{
					var timeSpent = item[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString();

					var timeSpentValue = 0m;

					Decimal.TryParse(timeSpent, out timeSpentValue);

					series[i++] = timeSpentValue;
				}

				var totalHoursWorkedForGroup = series.Sum();


				var mainDiv = new HtmlGenericControl("table");
				mainDiv.Attributes["class"] = "table table-bordered";

				// create tab for each sub group by distinct value
				foreach (var key in distinctFieldValues)
				{
					var detailContainer = new HtmlGenericControl("div");
					//detailContainer.Style.Add("Width", "100%");

					// Add DetailsWithChildrenControl
					var ctlDetailsWithChildren = Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl) as DetailsWithChildrenControl;

					ctlDetailsWithChildren.FieldConfigurationMode = ddlFieldConfigurationMode.SelectedValue;
					ctlDetailsWithChildren.SettingCategory = SettingCategory + "DetailsWithChildrenControl";
					ctlDetailsWithChildren.IsFCModeVisible = false;
					ctlDetailsWithChildren.Setup("Schedule", "Schedule", "ScheduleId", parentKey, subGroupByColumn, key, true, GetSubGroupData, GetColumns, "Schedule", DetailUserPreferenceCategoryId); 
					ctlDetailsWithChildren.SetVisibilityOfListFeatures(false, false, false);
					ctlDetailsWithChildren.SetSession("true");					
					//ctlDetailsWithChildren.Attributes.Add("width", "100%");

					//var filteredGroup = GetDataDetailsByKey(parentKey);
					var subGroupData = GetSubGroupData(parentKey, key);
					var item = ScheduleDataManager.GetStatisticData(subGroupData, ScheduleDataModel.DataColumns.ScheduleTimeSpentConstant, ApplicationCommon.ScheduleStatisticUnknown);

					var statisticControl = Page.LoadControl(ApplicationCommon.ScheduleStatisticControlPath) as ScheduleStatistics;
					statisticControl.SetStatistics(parentKey, item);

					detailContainer.Controls.Add(ctlDetailsWithChildren);
					detailContainer.Controls.Add(statisticControl);

					// add row for each sub grouping for statistic info tab
					var subDiv = GetStatisticInfoHTMLRow(item, key, totalHoursWorkedForGroup, filteredGroup.Rows.Count);
					mainDiv.Controls.Add(subDiv);
					
					// add to tab control
					tabControl.AddTab(key, detailContainer);

					if (Page.IsPostBack)
					{
						ctlDetailsWithChildren.ShowData(false, true);
					}
				}

				TableReportContent.Controls.Add(mainDiv);

				// add summary row for sub grouping for statistics info tab
				var ctrlStatSummary = GetStatisticInfoHTMLSummaryRow(filteredGroup, oSearchFilter.SubGroupBy);
				TableReportContent.Controls.Add(ctrlStatSummary);

				var seperator = new HtmlGenericControl("div");
				seperator.InnerHtml = "<br/>";
				TableReportContent.Controls.Add(seperator);

				if (detailsGridContainer != null)
				{
					detailsGridContainer.Controls.Add(tabControl);
				}

			}
			else // only Group By Case
			{
				// Add DetailsWithChildrenControl
				var ctlDetailsWithChildren = Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl) as DetailsWithChildrenControl;

				ctlDetailsWithChildren.ID = "oList";
				ctlDetailsWithChildren.SettingCategory = SettingCategory + "DetailsWithChildrenListControl";
				ctlDetailsWithChildren.IsFCModeVisible = false;
				ctlDetailsWithChildren.Setup("Schedule", "Schedule", "ScheduleId", parentKey, true, GetDataDetailsByKey, GetColumns, "Schedule", DetailUserPreferenceCategoryId);
				ctlDetailsWithChildren.SetVisibilityOfListFeatures(false, false, false);
				ctlDetailsWithChildren.SetSession("true");
				ctlDetailsWithChildren.FieldConfigurationMode = ddlFieldConfigurationMode.SelectedValue;

				if (detailsGridContainer != null)
				{
					detailsGridContainer.Controls.Add(ctlDetailsWithChildren);
				}

				if (Page.IsPostBack)
				{
					ctlDetailsWithChildren.ShowData(false, true);
				}

				var statisticControlGroup = Page.LoadControl(ApplicationCommon.ScheduleStatisticControlPath) as ScheduleStatistics;
				statisticControlGroup.SetStatistics(parentKey, statisticItem);

				if (detailsGridContainer != null)
				{
					detailsGridContainer.Controls.Add(statisticControlGroup);
				}

				HtmlGenericControl ctrlStat = null;
				if (oSearchFilter.GroupBy == "-1")
				{
					ctrlStat = GetStatisticInfoHTMLRow(statisticItem, parentKeyName, TotalTimeSpentCount, AllDataRows.Rows.Count);
				}
				else
				{
					ctrlStat = GetStatisticInfoHTMLRow(statisticItem, parentKey, TotalTimeSpentCount, AllDataRows.Rows.Count);
				}

				//var ctrlStat = oSG.GetStatisticDataGrid(statisticItems, parentKey);
				TableReportContent.Controls.Add(ctrlStat);

				// check if all rows created, if yes then add, summary row in the end.
				if (TotalParentCount == GrdParentGrid.Items.Count + 1)
				{
					var ctrlStatSummary = GetStatisticInfoHTMLSummaryRow(AllDataRows, oSearchFilter.GroupBy);
					TableReportContent.Controls.Add(ctrlStatSummary);
				}
			}

			if (oSearchFilter.GroupBy == "-1")
			{
				oSC.Setup(parentKeyName, statisticItem);
			}
			else
			{
				oSC.Setup(parentKey, statisticItem);
			}

		}

		#endregion

	}
}