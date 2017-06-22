using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TaskStatusType
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
                var taskStatusTypedata = new TaskStatusTypeDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.GetDetails(taskStatusTypedata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        taskStatusTypedata.TaskStatusTypeId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.GetDetails(taskStatusTypedata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    taskStatusTypedata.TaskStatusTypeId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.GetDetails(taskStatusTypedata, SessionVariables.RequestProfile);
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
            var data = new TaskStatusTypeDataModel();

            PropertyMapper.CopyProperties(data, values);

            TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskStatusType;
            PrimaryEntityKey = "TaskStatusType";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}