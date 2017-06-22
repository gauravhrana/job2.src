using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Drawing;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
	public partial class ScheduleStatChart : UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				ClearValues();
			}
		}

		protected void ClearValues()
		{
			statChart.Series.Clear();
		}

		public void Setup(string parentKey, Statistic item)
		{
			var totalSeries = statChart.Series.FindByName("Total");

			if (totalSeries == null)
				statChart.Series.Add("Total");

			statChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
			statChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
			statChart.ChartAreas[0].AxisX.Interval = 1;
			statChart.Series["Total"].Points.AddXY(parentKey, item.Total);
			statChart.Series["Total"].ChartType = SeriesChartType.Column;
			statChart.Series["Total"].BorderWidth = 3;
			statChart.Series["Total"].MarkerStyle = MarkerStyle.Circle;
			statChart.Series["Total"].IsValueShownAsLabel = true;

			var averageSeries = statChart.Series.FindByName("Average");

			if (averageSeries == null)
				statChart.Series.Add("Average");

			if (lstAverageItem.Checked)
			{
				statChart.Series["Average"].Points.AddXY(parentKey, item.Average);
				statChart.Series["Average"].ChartType = SeriesChartType.Line;
				statChart.Series["Average"].BorderWidth = 3;
				statChart.Series["Average"].BorderColor = Color.Red;
				statChart.Series["Average"].MarkerStyle = MarkerStyle.Circle;
				var averageLegend = statChart.Legends.FindByName("Average");
				if (averageLegend == null)
					statChart.Legends.Add("Average");
				statChart.Legends["Average"].Enabled = true;
			}

			var medianSeries = statChart.Series.FindByName("Median");
			if (medianSeries == null)
				statChart.Series.Add("Median");

			if (lstMedianItem.Checked)
			{
				statChart.Series["Median"].Points.AddXY(parentKey, item.Median);
				statChart.Series["Median"].ChartType = SeriesChartType.Line;
				statChart.Series["Median"].BorderWidth = 3;
				statChart.Series["Median"].BorderColor = Color.DarkViolet;
				statChart.Series["Median"].MarkerStyle = MarkerStyle.Circle;
				var medianLegend = statChart.Legends.FindByName("Median");
				if (medianLegend == null)
					statChart.Legends.Add("Median");
				statChart.Legends["Median"].Enabled = true;
			}

			var maxSeries = statChart.Series.FindByName("Max");
			if (maxSeries == null)
				statChart.Series.Add("Max");

			if (lstMaxItem.Checked)
			{
				statChart.Series["Max"].Points.AddXY(parentKey, item.Max);
				statChart.Series["Max"].ChartType = SeriesChartType.Line;
				statChart.Series["Max"].BorderWidth = 3;
				statChart.Series["Max"].BorderColor = Color.Brown;
				statChart.Series["Max"].MarkerStyle = MarkerStyle.Circle;
				var maxLegend = statChart.Legends.FindByName("Max");
				if (maxLegend == null)
					statChart.Legends.Add("Max");
				statChart.Legends["Max"].Enabled = true;
			}

			var minSeries = statChart.Series.FindByName("Min");
			if (minSeries == null)
				statChart.Series.Add("Min");

			if (lstMinItem.Checked)
			{
				statChart.Series["Min"].Points.AddXY(parentKey, item.Min);
				statChart.Series["Min"].ChartType = SeriesChartType.Line;
				statChart.Series["Min"].BorderWidth = 3;
				statChart.Series["Min"].BorderColor = Color.Green;
				statChart.Series["Min"].MarkerStyle = MarkerStyle.Circle;
				var minLegend = statChart.Legends.FindByName("Min");
				if (minLegend == null)
					statChart.Legends.Add("Min");
				statChart.Legends["Min"].Enabled = true;
			}
		}

		protected void statChart_Load(object sender, EventArgs e)
		{

		}
	}
}