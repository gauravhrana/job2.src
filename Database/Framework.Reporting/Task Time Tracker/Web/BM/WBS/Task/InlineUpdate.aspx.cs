using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.Task
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
				var taskdata = new TaskDataModel();

                selectedrows = TaskDataManager.GetDetails(taskdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        taskdata.TaskId = entityKey;
                        var result = TaskDataManager.GetDetails(taskdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
                    }
                }
				else 
				{
					taskdata.TaskId = SetId;
                    var result = TaskDataManager.GetDetails(taskdata, SessionVariables.RequestProfile);
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
			var data = new TaskDataModel();

            PropertyMapper.CopyProperties(data, values);

            TaskDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
            PrimaryEntity         = SystemEntity.Task;
			PrimaryEntityKey      = "Task";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;
			
			base.OnInit(e);
        }

        #endregion

    }
}
