using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskRole
{
    public partial class InlineUpdate : PageInlineUpdate
    {
        #region Methods

        protected override DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                var selectedrows = new DataTable();
                var taskRoledata = new TaskRoleDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.GetDetails(taskRoledata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        taskRoledata.TaskRoleId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.GetDetails(taskRoledata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    taskRoledata.TaskRoleId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.GetDetails(taskRoledata, SessionVariables.RequestProfile);
                    selectedrows.ImportRow(result.Rows[0]);

                }
                return selectedrows;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            return null;
        }

        protected override void Update(Dictionary<string, string> values)
        {
            var data = new TaskRoleDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRole;
            PrimaryEntityKey = "TaskRole";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}