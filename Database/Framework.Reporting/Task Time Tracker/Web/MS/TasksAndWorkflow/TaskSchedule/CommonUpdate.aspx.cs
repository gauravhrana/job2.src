using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.TasksAndWorkFlow;
using Framework.UI.Web.BaseClasses;
using Framework.Components.TasksAndWorkflow;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskSchedule
{
    public partial class CommonUpdate : PageCommonUpdate
    {
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new TaskScheduleDataModel();
			UpdatedData = TaskScheduleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskScheduleId =
					Convert.ToInt32(SelectedData.Rows[i][TaskScheduleDataModel.DataColumns.TaskScheduleId].ToString());

				data.TaskEntityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskScheduleDataModel.DataColumns.TaskEntityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskScheduleDataModel.DataColumns.TaskEntityId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskScheduleDataModel.DataColumns.TaskEntityId].ToString());


				data.TaskScheduleTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskScheduleDataModel.DataColumns.TaskScheduleTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskScheduleDataModel.DataColumns.TaskScheduleTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskScheduleDataModel.DataColumns.TaskScheduleTypeId].ToString());

				TaskScheduleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskScheduleDataModel();
				data.TaskScheduleId = Convert.ToInt32(SelectedData.Rows[i][TaskScheduleDataModel.DataColumns.TaskScheduleId].ToString());
				var dt = TaskScheduleDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var taskScheduledata = new TaskScheduleDataModel();
			taskScheduledata.TaskScheduleId = entityKey;
			var results = TaskScheduleDataManager.Search(taskScheduledata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore				= DynamicUpdatePanel;
			PrimaryEntity						= Framework.Components.DataAccess.SystemEntity.TaskSchedule;
			PrimaryEntityKey					= "TaskSchedule";
			BreadCrumbObject					= Master.BreadCrumbObject;
		}

		#endregion
    }
}