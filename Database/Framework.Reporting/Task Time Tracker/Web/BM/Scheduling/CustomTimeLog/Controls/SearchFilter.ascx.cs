using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		public CustomTimeLogDataModel SearchParameters
		{
			get
			{
				var data = new CustomTimeLogDataModel();

				SearchFilterControl.SetSearchParameters(data);

				var date = SearchFilterControl.CheckAndGetFieldValue(CustomTimeLogDataModel.DataColumns.PromotedDate).ToString();

				if (!string.IsNullOrEmpty(date))
				{
					var dates = date.Split('&');

					if (Boolean.Parse(dates[2]))
					{
						data.FromSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
						data.ToSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
					}
				}
				
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