﻿using System;
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

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskEntityType
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

                var selectedrows = new List<TaskEntityTypeDataModel>();
                var taskEntityTypedata = new TaskEntityTypeDataModel();
                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskEntityTypedata.TaskEntityTypeId = entityKey;
						var result = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetDetails(taskEntityTypedata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
                }
                else
                {
					taskEntityTypedata.TaskEntityTypeId = SetId;
					var result = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetDetails(taskEntityTypedata, SessionVariables.RequestProfile);
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
            var data = new TaskEntityTypeDataModel();
            
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntityType;
            PrimaryEntityKey = "TaskEntityType";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}

       