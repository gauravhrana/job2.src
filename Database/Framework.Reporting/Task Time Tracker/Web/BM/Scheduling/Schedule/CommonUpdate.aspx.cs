using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ScheduleDataModel();
            UpdatedData = ScheduleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ScheduleId =
					Convert.ToInt32(SelectedData.Rows[i][ScheduleDataModel.DataColumns.ScheduleId].ToString());

				data.PersonId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.PersonId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.PersonId).ToString())
					: int.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.PersonId].ToString());

				data.WorkDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.WorkDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.WorkDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.WorkDate].ToString());

				data.StartTime =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.StartTime))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.StartTime).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.StartTime].ToString());

				data.EndTime =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.EndTime))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.EndTime).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.EndTime].ToString());

				data.TotalHoursWorked =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.TotalHoursWorked))
					? decimal.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.TotalHoursWorked).ToString())
					: decimal.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.TotalHoursWorked].ToString());

				data.NextWorkDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.NextWorkDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.NextWorkDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.NextWorkDate].ToString());


				data.NextWorkTime =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.NextWorkTime))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.NextWorkTime).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.NextWorkTime].ToString());

				data.ScheduleStateId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.ScheduleStateId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDataModel.DataColumns.ScheduleStateId).ToString())
					: int.Parse(SelectedData.Rows[i][ScheduleDataModel.DataColumns.ScheduleStateId].ToString());

				data.CreatedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.CreatedDate].ToString());

                ScheduleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ScheduleDataModel();
				data.ScheduleId = Convert.ToInt32(SelectedData.Rows[i][ScheduleDataModel.DataColumns.ScheduleId].ToString());
                var dt = ScheduleDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var scheduledata = new ScheduleDataModel();
			scheduledata.ScheduleId = entityKey;
            var results = ScheduleDataManager.Search(scheduledata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.Schedule;
			PrimaryEntityKey = "Schedule";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

		
	}
}