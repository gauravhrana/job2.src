using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleState
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ScheduleStateDataModel();
            UpdatedData = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ScheduleStateId =
					Convert.ToInt32(SelectedData.Rows[i][ScheduleStateDataModel.DataColumns.ScheduleStateId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ScheduleStateDataModel();
				data.ScheduleStateId = Convert.ToInt32(SelectedData.Rows[i][ScheduleStateDataModel.DataColumns.ScheduleStateId].ToString());
                var dt = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}

			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var scheduleStatedata = new ScheduleStateDataModel();
			scheduleStatedata.ScheduleStateId = entityKey;
            var results = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.Search(scheduleStatedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleState;
			PrimaryEntityKey = "ScheduleState";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}