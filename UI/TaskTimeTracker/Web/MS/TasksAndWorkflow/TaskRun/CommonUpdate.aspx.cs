﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.TasksAndWorkFlow;
using Dapper;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskRun
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<TaskRunDataModel>();
            var data = new TaskRunDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TaskRunId =
                    Convert.ToInt32(SelectedData.Rows[i][TaskRunDataModel.DataColumns.TaskRunId].ToString());

                data.TaskEntityId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskRunDataModel.DataColumns.TaskEntityId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskRunDataModel.DataColumns.TaskEntityId).ToString())
                    : int.Parse(SelectedData.Rows[i][TaskRunDataModel.DataColumns.TaskEntityId].ToString());

                data.TaskScheduleId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskRunDataModel.DataColumns.TaskScheduleId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskRunDataModel.DataColumns.TaskScheduleId).ToString())
                    : int.Parse(SelectedData.Rows[i][TaskRunDataModel.DataColumns.TaskScheduleId].ToString());

                data.RunBy =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskRunDataModel.DataColumns.RunBy))
                    ? CheckAndGetRepeaterTextBoxValue(TaskRunDataModel.DataColumns.RunBy)
                    : SelectedData.Rows[i][TaskRunDataModel.DataColumns.RunBy].ToString();

				Framework.Components.TasksAndWorkflow.TaskRunDataManager.Update(data, SessionVariables.RequestProfile);
                data = new TaskRunDataModel();
                data.TaskRunId = Convert.ToInt32(SelectedData.Rows[i][TaskRunDataModel.DataColumns.TaskRunId].ToString());
				var dt = Framework.Components.TasksAndWorkflow.TaskRunDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }       

        protected override DataTable GetEntityData(int? entityKey)
        {
            var taskRundata = new TaskRunDataModel();
            taskRundata.TaskRunId = entityKey;
			var results = Framework.Components.TasksAndWorkflow.TaskRunDataManager.GetEntityDetails(taskRundata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRun;
            PrimaryEntityKey = "TaskRun";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}