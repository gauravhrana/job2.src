using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleHistory.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
		#region variables

		public ScheduleHistoryDataModel SearchParameters
		{
			get
			{
				var data = new ScheduleHistoryDataModel();


				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   ScheduleHistoryDataModel.DataColumns.ScheduleId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(
					   ScheduleHistoryDataModel.DataColumns.ScheduleId) != "")
				{
					data.ScheduleId = Convert.ToInt32(CheckAndGetFieldValue(
						ScheduleHistoryDataModel.DataColumns.ScheduleId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   ScheduleHistoryDataModel.DataColumns.Person + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(ScheduleHistoryDataModel.DataColumns.Person) != "")
				{
					data.Person = CheckAndGetFieldValue(ScheduleHistoryDataModel.DataColumns.Person).ToString();
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   ScheduleHistoryDataModel.DataColumns.ScheduleStateName + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(ScheduleHistoryDataModel.DataColumns.ScheduleStateName) != "")
				{
					data.ScheduleStateName = CheckAndGetFieldValue(ScheduleHistoryDataModel.DataColumns.ScheduleStateName).ToString();
				}

				return data;
			}
		}

		#endregion	

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ScheduleHistory";
			FolderLocationFromRoot = "Scheduling/ScheduleHistory";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleHistory;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}