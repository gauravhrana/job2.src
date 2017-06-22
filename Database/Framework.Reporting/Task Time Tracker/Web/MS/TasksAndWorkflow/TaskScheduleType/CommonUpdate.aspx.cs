﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskScheduleType
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new TaskScheduleTypeDataModel();
			UpdatedData = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TaskScheduleTypeId =
                    Convert.ToInt32(SelectedData.Rows[i][TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId].ToString());

                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                data.Active =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskScheduleTypeDataModel.DataColumns.Active))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskScheduleTypeDataModel.DataColumns.Active).ToString())
                    : int.Parse(SelectedData.Rows[i][TaskScheduleTypeDataModel.DataColumns.Active].ToString());

				Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Update(data, SessionVariables.RequestProfile);
                data = new TaskScheduleTypeDataModel();
                data.TaskScheduleTypeId = Convert.ToInt32(SelectedData.Rows[i][TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId].ToString());
				var dt = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }      

        protected override DataTable GetEntityData(int? entityKey)
        {
            var taskScheduleTypedata = new TaskScheduleTypeDataModel();
            taskScheduleTypedata.TaskScheduleTypeId = entityKey;
			var results = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Search(taskScheduleTypedata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskScheduleType;
            PrimaryEntityKey = "TaskScheduleType";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}