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
	public partial class InlineUpdate : PageInlineUpdate
    {
		#region Methods

		protected override DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                var selectedrows = new List<TaskRunDataModel>();
                var taskRundata = new TaskRunDataModel();
                
                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskRundata.TaskRunId = entityKey;
						var result = Framework.Components.TasksAndWorkflow.TaskRunDataManager.GetDetails(taskRundata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
                }
                else
                {
					taskRundata.TaskRunId = SetId;
					var result = Framework.Components.TasksAndWorkflow.TaskRunDataManager.GetDetails(taskRundata, SessionVariables.RequestProfile);
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
            var data = new TaskRunDataModel();

            PropertyMapper.CopyProperties(data, values);

			Framework.Components.TasksAndWorkflow.TaskRunDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRun;
            PrimaryEntityKey = "TaskRun";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}

       
