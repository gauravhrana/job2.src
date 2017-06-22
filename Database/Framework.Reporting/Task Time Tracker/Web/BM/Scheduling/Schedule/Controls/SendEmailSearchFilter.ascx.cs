using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Text;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
	public partial class SendEmailSearchFilter : ControlSearchFilterEntity
	{
		#region variables

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
				
				//if (SearchFilterControl.CheckIfValueIsValidAsInt(SearchFilterControl.CheckAndGetFieldValue(ScheduleDataModel.DataColumns.PersonId).ToString()) != null)
				//	data.PersonId = SearchFilterControl.GetParameterValueAsInt(ScheduleDataModel.DataColumns.PersonId);

				//if (SearchFilterControl.CheckIfValueIsValidAsInt(SearchFilterControl.CheckAndGetFieldValue(ScheduleDataModel.DataColumns.ScheduleStateId).ToString()) != null)
				//	data.ScheduleStateId = SearchFilterControl.GetParameterValueAsInt(ScheduleDataModel.DataColumns.ScheduleStateId);
				
				//var date = SearchFilterControl.CheckAndGetFieldValue(ScheduleDataModel.DataColumns.WorkDate).ToString();

				//if (!string.IsNullOrEmpty(date))
				//{
				//	var dates = date.Split('&');

				//	if (Boolean.Parse(dates[2]))
				//	{
				//		data.FromSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
				//		data.ToSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
				//	}
				//}

				SearchFilterControl.SetSearchParameters(data);

				var searchDates = SearchFilterControl.GetParameterValueForDatePanel(ScheduleDataModel.DataColumns.WorkDate);
				data.FromSearchDate = searchDates.Count > 0 ? searchDates[0] : null;
				data.ToSearchDate = searchDates.Count > 1 ? searchDates[1] : null;

				return data;
			}

		}

		#endregion		

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			base.BaseSearchFilterControl = SearchFilterControl;

			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		#endregion

	}
}