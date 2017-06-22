using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskEntity
{
    public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new TaskEntityDataModel();
			UpdatedData = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskEntityId =
					Convert.ToInt32(SelectedData.Rows[i][TaskEntityDataModel.DataColumns.TaskEntityId].ToString());

				data.TaskEntityTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskEntityDataModel.DataColumns.TaskEntityTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskEntityDataModel.DataColumns.TaskEntityTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskEntityDataModel.DataColumns.TaskEntityTypeId].ToString());

				data.Name =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name).ToString()
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description).ToString()
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.Active =
						!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskEntityDataModel.DataColumns.Active))
						? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskEntityDataModel.DataColumns.Active).ToString())
						: int.Parse(SelectedData.Rows[i][TaskEntityDataModel.DataColumns.Active].ToString());

				Framework.Components.TasksAndWorkflow.TaskEntityDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskEntityDataModel();
				data.TaskEntityId = Convert.ToInt32(SelectedData.Rows[i][TaskEntityDataModel.DataColumns.TaskEntityId].ToString());
				var dt = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var taskEntitydata = new TaskEntityDataModel();
			taskEntitydata.TaskEntityId = entityKey;
			var results = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.Search(taskEntitydata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntity;
			PrimaryEntityKey = "TaskEntity";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
        
    }
}