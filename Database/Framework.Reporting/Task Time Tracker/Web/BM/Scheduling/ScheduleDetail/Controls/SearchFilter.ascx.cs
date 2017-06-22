using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.ReleaseLog;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Text;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
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

		public ApplicationContainer.UI.Web.BaseUI.SearchFilterControl SearchControl
		{
			get
			{
				return SearchFilterControl;
			}
		}

		public ScheduleDetailDataModel SearchParameters
		{
			get
			{
				var data            = new ScheduleDetailDataModel();

				SearchFilterControl.SetSearchParameters(data);

				var searchDates     = SearchFilterControl.GetParameterValueForDatePanel(ScheduleDetailDataModel.DataColumns.WorkDate);
				data.FromSearchDate = searchDates.Count > 0 ? searchDates[0] : null;
				data.ToSearchDate   = searchDates.Count > 1 ? searchDates[1] : null;

				return data;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl                         = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

		public string SettingCategory
		{
			get
			{
				return Convert.ToString(ViewState["SettingCategory"]);
			}
			set
			{
				ViewState["SettingCategory"] = value;
			}
		}
	}
}