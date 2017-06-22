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
using DataModel.Framework.TasksAndWorkFlow;

namespace ApplicationContainer.UI.Web.TasksAndWorkflow.TaskEntity
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
                var taskEntitydata = new TaskEntityDataModel();

				selectedrows = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetDetails(taskEntitydata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskEntitydata.TaskEntityId = entityKey;
						var result = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetDetails(taskEntitydata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
                }
                else
                {
					taskEntitydata.TaskEntityId = SetId;
					var result = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetDetails(taskEntitydata, SessionVariables.RequestProfile);
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
            var data = new TaskEntityDataModel();

            PropertyMapper.CopyProperties(data, values);

			Framework.Components.TasksAndWorkflow.TaskEntityDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntity;
            PrimaryEntityKey = "TaskEntity";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}