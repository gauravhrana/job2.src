using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new TaskAlgorithmItemDataModel();
            UpdatedData = TaskAlgorithmItemDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskAlgorithmItemId =
					Convert.ToInt32(SelectedData.Rows[i][TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId].ToString());

				data.ActivityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.ActivityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.ActivityId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskAlgorithmItemDataModel.DataColumns.ActivityId].ToString());

				data.TaskAlgorithmId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId].ToString());
                
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.Description)
					: SelectedData.Rows[i][TaskAlgorithmItemDataModel.DataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskAlgorithmItemDataModel.DataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][TaskAlgorithmItemDataModel.DataColumns.SortOrder].ToString());

                TaskAlgorithmItemDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskAlgorithmItemDataModel();
				data.TaskAlgorithmItemId = Convert.ToInt32(SelectedData.Rows[i][TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId].ToString());
                var dt = TaskAlgorithmItemDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var taskAlgorithmItemdata = new TaskAlgorithmItemDataModel();
			taskAlgorithmItemdata.TaskAlgorithmItemId = entityKey;
            var results = TaskAlgorithmItemDataManager.Search(taskAlgorithmItemdata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.TaskAlgorithmItem;
			PrimaryEntityKey = "TaskAlgorithmItem";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}