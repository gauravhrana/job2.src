using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Web.UI.HtmlControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
	public partial class ScheduleStatGrid : UserControl
	{
		decimal Total;
		decimal Average;
		decimal Median;
		int RecCount;
		decimal MaxValue;
		decimal MinValue;
		decimal count = 0;

		List<decimal> lstMedians = new List<decimal>();
		List<decimal> lstTotal = new List<decimal>();
		List<decimal> lstCount = new List<decimal>();
		List<decimal> lstAverage = new List<decimal>();
		List<decimal> lstMax = new List<decimal>();
		List<decimal> lstMin = new List<decimal>();

		protected void ClearValues()
		{
			Total = 0;
			RecCount = 0;
			Median = 0;
			Average = 0;

			lstTotal.Clear();
			lstCount.Clear();
			lstMax.Clear();
			lstMin.Clear();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ClearValues();
		}

		//public HtmlGenericControl GetStatisticSummary(EnumerableRowCollection<DataRow> scheduleData, Statistic item, string key, string groupName)
		//{
		//	lblHeader.Text = groupName;
		//	var lstValues = ScheduleDataManager.GetStatisticDataSummary(scheduleData, ScheduleDataModel.DataColumns.ScheduleTimeSpentConstant, ApplicationCommon.ScheduleStatisticUnknown);
		//	Average = lstValues["Average"];
		//	Median = lstValues["Median"];

		//	Total = (decimal)lstTotal.Sum();
		//	RecCount = (int)lstCount.Sum();

		//	if (lstMax.Count != 0)
		//		MaxValue = (decimal)lstMax.Max();

		//	if (lstMin.Count != 0)
		//		MinValue = (decimal)lstMin.Min();

		//	var mainDiv1 = new HtmlGenericControl("table");

		//	var subDiv1 = new HtmlGenericControl("tr");
		//	subDiv1.Attributes["class"] = "rowActive";

		//	var divId1 = new HtmlGenericControl("td");
		//	divId1.Attributes["class"] = "cell";

		//	var lblScheduleText1 = new Label();
		//	lblScheduleText1.Text = "Summary";
		//	divId1.Controls.Add(lblScheduleText1);
		//	subDiv1.Controls.Add(divId1);

		//	var divTotal1 = new HtmlGenericControl("td");
		//	divTotal1.Attributes["class"] = "cell";

		//	var lblTotalText1 = new Label();
		//	lblTotalText1.Text = Total.ToString("#0,0.00");
		//	divTotal1.Controls.Add(lblTotalText1);
		//	subDiv1.Controls.Add(divTotal1);

		//	var divTotalPer = new HtmlGenericControl("td");
		//	divTotalPer.Attributes["class"] = "cell";

		//	var lblTotalPerText = new Label();
		//	lblTotalPerText.Text = "100%";
		//	divTotalPer.Controls.Add(lblTotalPerText);
		//	subDiv1.Controls.Add(divTotalPer);

		//	var divAverage1 = new HtmlGenericControl("td");
		//	divAverage1.Attributes["class"] = "cell";

		//	var lblAverageText1 = new Label();
		//	lblAverageText1.Text = Average.ToString("#0,0.00");
		//	divAverage1.Controls.Add(lblAverageText1);
		//	subDiv1.Controls.Add(divAverage1);

		//	var divMedianSummary = new HtmlGenericControl("td");
		//	divMedianSummary.Attributes["class"] = "cell";

		//	var lblMedianText1 = new Label();
		//	lblMedianText1.Text = Median.ToString("#0,0.00");
		//	divMedianSummary.Controls.Add(lblMedianText1);
		//	subDiv1.Controls.Add(divMedianSummary);

		//	var divCountSummary = new HtmlGenericControl("td");
		//	divCountSummary.Attributes["class"] = "cell";

		//	var lblCountText1 = new Label();
		//	lblCountText1.Text = RecCount.ToString("#0,0.00");
		//	divCountSummary.Controls.Add(lblCountText1);
		//	subDiv1.Controls.Add(divCountSummary);

		//	var divCntPer = new HtmlGenericControl("td");
		//	divCntPer.Attributes["class"] = "cell";

		//	var lblCntPerText = new Label();
		//	lblCntPerText.Text = "100%";
		//	divCntPer.Controls.Add(lblCntPerText);
		//	subDiv1.Controls.Add(divCntPer);

		//	var divMaxSummary = new HtmlGenericControl("td");
		//	divMaxSummary.Attributes["class"] = "cell";

		//	var lblMaxText1 = new Label();
		//	lblMaxText1.Text = MaxValue.ToString("#0,0.00");
		//	divMaxSummary.Controls.Add(lblMaxText1);
		//	subDiv1.Controls.Add(divMaxSummary);

		//	var divMinSummary = new HtmlGenericControl("td");
		//	divMinSummary.Attributes["class"] = "cell";

		//	var lblMinText1 = new Label();
		//	lblMinText1.Text = MinValue.ToString("#0,0.00");
		//	divMinSummary.Controls.Add(lblMinText1);
		//	subDiv1.Controls.Add(divMinSummary);

		//	mainDiv1.Controls.Add(subDiv1);
		//	return mainDiv1;
		//}
		
		public HtmlGenericControl GetStatisticDataGrid(List<Statistic> statisticItems, string key, decimal totalHoursCount = 0, decimal totalRowCount = 0)
		{
			var item = statisticItems[0];
			var style = StyleStat();

			var mainDiv = new HtmlGenericControl("table");
			mainDiv.Attributes.Add("class", "table");

			var subDiv = new HtmlGenericControl("tr");
			subDiv.Attributes["class"] = style;

			var divId = new HtmlGenericControl("td");
			divId.Attributes["class"] = "cell";


			var lblScheduleText = new Label();
			lblScheduleText.Text = key;
			divId.Controls.Add(lblScheduleText);
			subDiv.Controls.Add(divId);

			var divTotal = new HtmlGenericControl("td");
			divTotal.Attributes["class"] = "cell";
			var lblTotalText = new Label();
			lblTotalText.Text = ((decimal)item.Total).ToString("#0,0.00");

			divTotal.Controls.Add(lblTotalText);
			subDiv.Controls.Add(divTotal);
			lstTotal.Add((decimal)item.Total);

			if (totalHoursCount != 0)
			{
				//var per = (item.Total / totalHoursCount) * 100;
				//lblTotalText.Text += " (" + per.Value.ToString("#0.00") + "%)";

				item.TotalPercentage = (item.Total / totalHoursCount) * 100;

				var divTotalPercentage = new HtmlGenericControl("td");
				divTotalPercentage.Attributes["class"] = "cell";

				var lblTotalPercentageText = new Label();
				lblTotalPercentageText.Text += item.TotalPercentage.Value.ToString("#0.00") + "%";

				divTotalPercentage.Controls.Add(lblTotalPercentageText);
				subDiv.Controls.Add(divTotalPercentage);
			}

			//divTotal.Controls.Add(lblTotalText);
			//subDiv.Controls.Add(divTotal);
			//lstTotal.Add((decimal)item.Total);

			//var divAverage = new HtmlGenericControl("td");
			//divAverage.Attributes["class"] = "cell";

			//var lblAverageText = new Label();
			//lblAverageText.Text = Math.Round(Convert.ToDecimal(item.Average), 2).ToString("#0,0.00");
			//divAverage.Controls.Add(lblAverageText);
			//subDiv.Controls.Add(divAverage);

			//var divMedian = new HtmlGenericControl("td");
			//divMedian.Attributes["class"] = "cell";

			//var lblMedianText = new Label();
			//if (item.Median != null)
			//	lblMedianText.Text = ((decimal)item.Median).ToString("#0,0.00");
			//divMedian.Controls.Add(lblMedianText);
			//subDiv.Controls.Add(divMedian);

			//var divCount = new HtmlGenericControl("td");
			//divCount.Attributes["class"] = "cell";

			//var lblCountText = new Label();
			//lblCountText.Text = ((decimal)item.Count).ToString("#0,0.00");

			var divAverage = new HtmlGenericControl("td");
			divAverage.Attributes["class"] = "cell";

			var lblAverageText = new Label();
			lblAverageText.Text = Math.Round(Convert.ToDecimal(item.Average), 2).ToString("#0,0.00");
			divAverage.Controls.Add(lblAverageText);
			subDiv.Controls.Add(divAverage);

			var divMedian = new HtmlGenericControl("td");
			divMedian.Attributes["class"] = "cell";

			var lblMedianText = new Label();
			if (item.Median != null)
				lblMedianText.Text = ((decimal)item.Median).ToString("#0,0.00");
			divMedian.Controls.Add(lblMedianText);
			subDiv.Controls.Add(divMedian);

			var divCount = new HtmlGenericControl("td");
			divCount.Attributes["class"] = "cell";
			var lblCountText = new Label();
			lblCountText.Text = ((decimal)item.Count).ToString("#0,0.00");
			divCount.Controls.Add(lblCountText);
			subDiv.Controls.Add(divCount);
			lstCount.Add((decimal)item.Count);

			if (totalRowCount != 0)
			{
				item.CountPercentage = (item.Count / totalRowCount) * 100;
				var divCountPercentage = new HtmlGenericControl("td");
				divCountPercentage.Attributes["class"] = "cell";

				var lblCountPercentageText = new Label();
				lblCountPercentageText.Text += item.CountPercentage.Value.ToString("#0.00") + "%";

				divCountPercentage.Controls.Add(lblCountPercentageText);
				subDiv.Controls.Add(divCountPercentage);
			}

			var divMax = new HtmlGenericControl("td");
			divMax.Attributes["class"] = "cell";

			var lblMaxText = new Label();
			if (item.Max != null)
				lblMaxText.Text = ((decimal)item.Max).ToString("#0,0.00");
			divMax.Controls.Add(lblMaxText);
			subDiv.Controls.Add(divMax);
			if (item.Max != null)
				lstMax.Add((decimal)item.Max);

			var divMin = new HtmlGenericControl("td");
			divMin.Attributes["class"] = "cell";

			var lblMinText = new Label();
			if (item.Min != null)
				lblMinText.Text = ((decimal)item.Min).ToString("#0,0.00");
			divMin.Controls.Add(lblMinText);
			subDiv.Controls.Add(divMin);
			if (item.Min != null)
				lstMin.Add((decimal)item.Min);

			mainDiv.Controls.Add(subDiv);

			//TableReportContent.Controls.Add(mainDiv);
			return mainDiv;

			//divCount.Controls.Add(lblCountText);
			//subDiv.Controls.Add(divCount);
			//lstCount.Add((decimal)item.Count);

			//var divMax = new HtmlGenericControl("td");
			//divMax.Attributes["class"] = "cell";

			//var lblMaxText = new Label();
			//if (item.Max != null)
			//	lblMaxText.Text = ((decimal)item.Max).ToString("#0,0.00");
			//divMax.Controls.Add(lblMaxText);
			//subDiv.Controls.Add(divMax);
			//if (item.Max != null)
			//	lstMax.Add((decimal)item.Max);

			//var divMin = new HtmlGenericControl("td");
			//divMin.Attributes["class"] = "cell";

			//var lblMinText = new Label();
			//if (item.Min != null)
			//	lblMinText.Text = ((decimal)item.Min).ToString("#0,0.00");
			//divMin.Controls.Add(lblMinText);
			//subDiv.Controls.Add(divMin);
			//if (item.Min != null)
			//	lstMin.Add((decimal)item.Min);

			//mainDiv.Controls.Add(subDiv);

			////TableReportContent.Controls.Add(mainDiv);
			//return mainDiv;

			//var divTotal = new HtmlGenericControl("div");
			//divTotal.Attributes["class"] = "cell";			
			//var lblTotalText = new Label();
			//lblTotalText.Text = item.Total.ToString();
			//divTotal.Controls.Add(lblTotalText);
			//subDiv.Controls.Add(divTotal);
			//lstTotal.Add((decimal)item.Total);

			//var divAverage = new HtmlGenericControl("div");
			//divAverage.Attributes["class"] = "cell";

			//var lblAverageText = new Label();
			//lblAverageText.Text = Math.Round(Convert.ToDecimal(item.Average), 2).ToString();
			//divAverage.Controls.Add(lblAverageText);
			//subDiv.Controls.Add(divAverage);

			//var divMedian = new HtmlGenericControl("div");
			//divMedian.Attributes["class"] = "cell";

			//var lblMedianText = new Label();
			//lblMedianText.Text = item.Median.ToString();
			//divMedian.Controls.Add(lblMedianText);
			//subDiv.Controls.Add(divMedian);

			//var divCount = new HtmlGenericControl("div");
			//divCount.Attributes["class"] = "cell";

			//var lblCountText = new Label();
			//lblCountText.Text = item.Count.ToString();
			//divCount.Controls.Add(lblCountText);
			//subDiv.Controls.Add(divCount);
			//lstCount.Add((decimal)item.Count);

			//var divMax = new HtmlGenericControl("div");
			//divMax.Attributes["class"] = "cell";

			//var lblMaxText = new Label();
			//lblMaxText.Text = item.Max.ToString();
			//divMax.Controls.Add(lblMaxText);
			//subDiv.Controls.Add(divMax);
			//lstMax.Add((decimal)item.Max);

			//var divMin = new HtmlGenericControl("div");
			//divMin.Attributes["class"] = "cell";
			//var lblMinText = new Label();
			//lblMinText.Text = item.Min.ToString();
			//divMin.Controls.Add(lblMinText);
			//subDiv.Controls.Add(divMin);
			//lstMin.Add((decimal)item.Min);

			//mainDiv.Controls.Add(subDiv);

			////TableReportContent.Controls.Add(mainDiv);
			//return mainDiv;

		}

		public string StyleStat()
		{
			count++;

			var style = string.Empty;

			if (count % 2 == 0)

				style = "rowRN";
			else
				style = "rowAlternate";

			return style;
		}

	}
}