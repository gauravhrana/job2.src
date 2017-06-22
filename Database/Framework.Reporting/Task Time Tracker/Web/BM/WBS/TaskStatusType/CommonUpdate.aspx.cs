using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.WBS.TaskStatusType
{
    public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new TaskStatusTypeDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskStatusTypeId =
					Convert.ToInt32(SelectedData.Rows[i][TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskStatusTypeDataModel();
				data.TaskStatusTypeId = Convert.ToInt32(SelectedData.Rows[i][TaskStatusTypeDataModel.DataColumns.TaskStatusTypeId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}				
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var taskStatusTypedata = new TaskStatusTypeDataModel();
			taskStatusTypedata.TaskStatusTypeId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.Search(taskStatusTypedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskStatusType;
			PrimaryEntityKey = "TaskStatusType";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
    }
}