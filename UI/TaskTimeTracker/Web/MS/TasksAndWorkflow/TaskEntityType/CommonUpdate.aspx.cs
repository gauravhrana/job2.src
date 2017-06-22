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
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Dapper;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskEntityType
{
    public partial class CommonUpdate : PageCommonUpdate
    {        
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<TaskEntityTypeDataModel>();
            var data = new TaskEntityTypeDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TaskEntityTypeId =
                    Convert.ToInt32(SelectedData.Rows[i][TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId].ToString());
                
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
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskEntityTypeDataModel.DataColumns.Active))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskEntityTypeDataModel.DataColumns.Active).ToString())
                    : int.Parse(SelectedData.Rows[i][TaskEntityTypeDataModel.DataColumns.Active].ToString());

				Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.Update(data, SessionVariables.RequestProfile);
                data = new TaskEntityTypeDataModel();
                data.TaskEntityTypeId = Convert.ToInt32(SelectedData.Rows[i][TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId].ToString());
				var dt = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }       

        protected override DataTable GetEntityData(int? entityKey)
        {
            var taskEntityTypedata = new TaskEntityTypeDataModel();
            taskEntityTypedata.TaskEntityTypeId = entityKey;
			var results = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetEntityDetails(taskEntityTypedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntityType;
            PrimaryEntityKey = "TaskEntityType";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}