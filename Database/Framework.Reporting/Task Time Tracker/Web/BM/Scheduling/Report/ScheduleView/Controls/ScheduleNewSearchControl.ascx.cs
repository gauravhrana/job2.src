using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Text;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.BM.Scheduling.Report.ScheduleView.Controls
{

	public partial class ScheduleNewSearchControl : Framework.UI.Web.BaseClasses.ControlSearchFilterEntity
	{

		public string GroupBy
		{
			get
			{
				return SearchFilterControl.GroupBy;
			}
		}

		public string SubGroupBy
		{
			get
			{
				return SearchFilterControl.SubGroupBy;
			}
		}

		public string GroupByDirection
		{
			get
			{
				return SearchFilterControl.GroupByDirection;
			}
		}

		

		public ApplicationContainer.UI.Web.BaseUI.SearchFilterControl SearchControl
		{
			get
			{
				return SearchFilterControl;
			}
		}

		public ScheduleDataModel SearchParameters
		{
			get
			{
				var data = new ScheduleDataModel();

				SearchFilterControl.SetSearchParameters(data);

				//var excludeItems	=
				var workDates = SearchFilterControl.GetParameterValueForDatePanel(ScheduleDataModel.DataColumns.WorkDate);
				data.FromSearchDate = workDates.Count > 0 ? workDates[0] : null;
				data.ToSearchDate = workDates.Count > 1 ? workDates[1] : null;
				CommonSearchParameters();

				return data;
			}

		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

	}
}