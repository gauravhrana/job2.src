using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.Task;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()	
		{
			var UpdatedData = new DataTable();
			var data = new TaskPackageXOwnerXTaskDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskPackageXOwnerXTaskId =
					Convert.ToInt32(SelectedData.Rows[i][TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId].ToString());
				
				data.TaskPackageId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageId].ToString());

				data.ApplicationUserId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskPackageXOwnerXTaskDataModel.DataColumns.ApplicationUserId].ToString());
			
				data.TaskId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskPackageXOwnerXTaskDataModel.DataColumns.TaskId].ToString());


                TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskPackageXOwnerXTaskDataModel();
				data.TaskPackageXOwnerXTaskId = Convert.ToInt32(SelectedData.Rows[i][TaskPackageXOwnerXTaskDataModel.DataColumns.TaskPackageXOwnerXTaskId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var taskPackageXOwnerXTaskdata = new TaskPackageXOwnerXTaskDataModel();
            taskPackageXOwnerXTaskdata.TaskPackageXOwnerXTaskId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Search(taskPackageXOwnerXTaskdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask;
            PrimaryEntityKey = "TaskPackageXOwnerXTask";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}