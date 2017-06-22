using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskScheduleType
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

                var selectedrows = new List<TaskScheduleTypeDataModel>();
                var taskScheduleTypedata = new TaskScheduleTypeDataModel();

                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        taskScheduleTypedata.TaskScheduleTypeId = entityKey;
						var result = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.GetDetails(taskScheduleTypedata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
                    }
                }
                else
                {
                    taskScheduleTypedata.TaskScheduleTypeId = SetId;
					var result = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.GetDetails(taskScheduleTypedata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);

                }
                return selectedrows.ToDataTable();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

        protected override void Update(Dictionary<string, string> values)
        {
            var data = new TaskScheduleTypeDataModel();

            PropertyMapper.CopyProperties(data, values);

			Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Update(data, SessionVariables.RequestProfile);

            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskScheduleType;
            PrimaryEntityKey = "TaskScheduleType";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}


