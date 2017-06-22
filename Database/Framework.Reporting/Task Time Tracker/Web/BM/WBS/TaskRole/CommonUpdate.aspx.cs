using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.TaskRole
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new TaskRoleDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TaskRoleId =
                    Convert.ToInt32(SelectedData.Rows[i][TaskRoleDataModel.DataColumns.TaskRoleId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                data.ApplicationId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId).ToString())
                    : int.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());

                TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Save(data, "Update", SessionVariables.RequestProfile);
                data = new TaskRoleDataModel();
                data.TaskRoleId = Convert.ToInt32(SelectedData.Rows[i][TaskRoleDataModel.DataColumns.TaskRoleId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var taskRoledata = new TaskRoleDataModel();
            taskRoledata.TaskRoleId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Search(taskRoledata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRole;
            PrimaryEntityKey = "TaskRole";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}