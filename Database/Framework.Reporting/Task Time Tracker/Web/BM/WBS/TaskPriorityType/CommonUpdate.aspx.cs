using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Priority;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityType
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new TaskPriorityTypeDataModel();
            UpdatedData = TaskPriorityTypeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskPriorityTypeId =
					Convert.ToInt32(SelectedData.Rows[i][TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.Weight =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskPriorityTypeDataModel.DataColumns.Weight))
					? decimal.Parse(CheckAndGetRepeaterTextBoxValue(TaskPriorityTypeDataModel.DataColumns.Weight))
					: decimal.Parse(SelectedData.Rows[i][TaskPriorityTypeDataModel.DataColumns.Weight].ToString());

               TaskPriorityTypeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskPriorityTypeDataModel();
				data.TaskPriorityTypeId = Convert.ToInt32(SelectedData.Rows[i][TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId].ToString());
                var dt = TaskPriorityTypeDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}			
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var taskPriorityTypedata = new TaskPriorityTypeDataModel();
			taskPriorityTypedata.TaskPriorityTypeId = entityKey;
            var results = TaskPriorityTypeDataManager.Search(taskPriorityTypedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPriorityType;
			PrimaryEntityKey = "TaskPriorityType";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	
       
	}
}