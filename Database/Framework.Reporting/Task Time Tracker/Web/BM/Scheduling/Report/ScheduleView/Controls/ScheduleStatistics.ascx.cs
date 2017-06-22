using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;

namespace ApplicationContainer.UI.Web.BM.Scheduling.Report.ScheduleView.Controls
{
	public partial class ScheduleStatistics : UserControl
	{
		#region Methods

		public void SetStatistics(string parentKey, Statistic item)
		{

			lblCount.Text = item.Total.ToString();

			lblAverageValue.Text = Math.Round(Convert.ToDecimal(item.Average), 2).ToString();

			lblMedianValue.Text = item.Median.ToString();

			lblRecordCount.Text = item.Count.ToString();

			lblMax.Text = item.Max.ToString();

			lblMin.Text = item.Min.ToString();

		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		#endregion
	}
}