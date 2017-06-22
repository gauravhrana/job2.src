using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner.Controls
{
	public partial class MOStatGrid : System.Web.UI.UserControl
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
			//TableReportContent.Controls.Clear();
			//TableReportContent.Controls.Add(StatsDiv);
			//plcGroupByHolder.Controls.Clear();

			Total = 0;
			RecCount = 0;
			Median = 0;
			Average = 0;

			lstTotal.Clear();
			lstCount.Clear();
			lstMax.Clear();
			lstMin.Clear();

			//statChart.Series.Clear();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ClearValues();
		}

		public HtmlGenericControl GetStatisticSummary(EnumerableRowCollection<DataRow> scheduleData, Statistic item, string key, string groupName)
		{
			lblHeader.Text = groupName;
			var lstValues = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetStatisticDataSummary(scheduleData, ModuleOwnerDataModel.DataColumns.ModuleOwnerTimeSpentConstant);
			Average = Math.Round(lstValues[0], 2);
			Median = lstValues[1];

			Total = (decimal)lstTotal.Sum();
			RecCount = (int)lstCount.Sum();

			if (lstMax.Count != 0)
				MaxValue = (decimal)lstMax.Max();

			if (lstMin.Count != 0)
				MinValue = (decimal)lstMin.Min();

			//HtmlGenericControl mainDiv1 = new HtmlGenericControl("div");
			HtmlGenericControl mainDiv1 = new HtmlGenericControl("table");
			//mainDiv1.Attributes["class"] = "table";

			//HtmlGenericControl subDiv1 = new HtmlGenericControl("div");
			HtmlGenericControl subDiv1 = new HtmlGenericControl("tr");
			subDiv1.Attributes["class"] = "rowActive";
			//subDiv1.Attributes["class"] = "row";

			HtmlGenericControl divId1 = new HtmlGenericControl("td");
			divId1.Attributes["class"] = "cell";

			Label lblScheduleText1 = new Label();
			lblScheduleText1.Text = "Summary";
			divId1.Controls.Add(lblScheduleText1);
			subDiv1.Controls.Add(divId1);

			//HtmlGenericControl divTotal1 = new HtmlGenericControl("div");
			HtmlGenericControl divTotal1 = new HtmlGenericControl("td");
			divTotal1.Attributes["class"] = "cell";

			Label lblTotalText1 = new Label();
			//lblTotalText1.Text = Total.ToString();
			lblTotalText1.Text = Total.ToString("#0,0.00");
			divTotal1.Controls.Add(lblTotalText1);
			subDiv1.Controls.Add(divTotal1);

			HtmlGenericControl divAverage1 = new HtmlGenericControl("td");
			//HtmlGenericControl divAverage1 = new HtmlGenericControl("div");
			divAverage1.Attributes["class"] = "cell";

			Label lblAverageText1 = new Label();
			//lblAverageText1.Text = Average.ToString();
			lblAverageText1.Text = Average.ToString("#0,0.00");
			divAverage1.Controls.Add(lblAverageText1);
			subDiv1.Controls.Add(divAverage1);

			//HtmlGenericControl divMedianSummary = new HtmlGenericControl("div");
			HtmlGenericControl divMedianSummary = new HtmlGenericControl("td");
			divMedianSummary.Attributes["class"] = "cell";

			Label lblMedianText1 = new Label();
			//lblMedianText1.Text = Median.ToString();
			lblMedianText1.Text = Median.ToString("#0,0.00");
			divMedianSummary.Controls.Add(lblMedianText1);
			subDiv1.Controls.Add(divMedianSummary);

			//HtmlGenericControl divCountSummary = new HtmlGenericControl("div");
			HtmlGenericControl divCountSummary = new HtmlGenericControl("td");
			divCountSummary.Attributes["class"] = "cell";

			Label lblCountText1 = new Label();
			//lblCountText1.Text = RecCount.ToString();
			lblCountText1.Text = RecCount.ToString("#0,0.00");
			divCountSummary.Controls.Add(lblCountText1);
			subDiv1.Controls.Add(divCountSummary);

			//HtmlGenericControl divMaxSummary = new HtmlGenericControl("div");
			HtmlGenericControl divMaxSummary = new HtmlGenericControl("td");
			divMaxSummary.Attributes["class"] = "cell";

			Label lblMaxText1 = new Label();
			//lblMaxText1.Text = MaxValue.ToString();
			lblMaxText1.Text = MaxValue.ToString("#0,0.00");
			divMaxSummary.Controls.Add(lblMaxText1);
			subDiv1.Controls.Add(divMaxSummary);

			//HtmlGenericControl divMinSummary = new HtmlGenericControl("div");
			HtmlGenericControl divMinSummary = new HtmlGenericControl("td");
			divMinSummary.Attributes["class"] = "cell";

			Label lblMinText1 = new Label();
			//lblMinText1.Text = MinValue.ToString();
			lblMinText1.Text = MinValue.ToString("#0,0.00");
			divMinSummary.Controls.Add(lblMinText1);
			subDiv1.Controls.Add(divMinSummary);

			mainDiv1.Controls.Add(subDiv1);
			return mainDiv1;
			//TableReportContent.Controls.Add(mainDiv1);

		}


		public HtmlGenericControl GetStatisticDataGrid(List<Statistic> statisticItems, string key)
		{
			var item = statisticItems[0];
			var style = StyleStat();

			//var mainDiv = new HtmlGenericControl("div");
			//mainDiv.Attributes["class"] = "table";
			var mainDiv = new HtmlGenericControl("table");

			//var subDiv = new HtmlGenericControl("div");
			//subDiv.Attributes["class"] = "row";
			var subDiv = new HtmlGenericControl("tr");
			subDiv.Attributes["class"] = style;

			var divId = new HtmlGenericControl("td");
			divId.Attributes["class"] = "cell";

			//var divId = new HtmlGenericControl("div");
			//divId.Attributes["class"] = "cell";

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

		}

		public string StyleStat()
		{
			count++;

			string style = string.Empty;

			if (count % 2 == 0)

				style = "rowRN";
			else
				style = "rowAlternate";

			return style;
		}
	}
}