using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.WBS.Task
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new TaskDataModel();
            UpdatedData = TaskDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskId =
					Convert.ToInt32(SelectedData.Rows[i][TaskDataModel.DataColumns.TaskId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.TaskTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskDataModel.DataColumns.TaskTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskDataModel.DataColumns.TaskTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskDataModel.DataColumns.TaskTypeId].ToString());

                TaskDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskDataModel();
				data.TaskId = Convert.ToInt32(SelectedData.Rows[i][TaskDataModel.DataColumns.TaskId].ToString());
                var dt = TaskDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}				       
			}
			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var taskdata = new TaskDataModel();
			taskdata.TaskId = entityKey;
            var results = TaskDataManager.Search(taskdata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.Task;
			PrimaryEntityKey = "Task";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}