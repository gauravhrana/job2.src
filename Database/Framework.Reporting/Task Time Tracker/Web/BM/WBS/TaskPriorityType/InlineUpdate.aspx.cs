using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Priority;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.TaskPriorityType
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
				var taskPriorityTypedata = new TaskPriorityTypeDataModel();

                selectedrows = TaskPriorityTypeDataManager.GetDetails(taskPriorityTypedata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskPriorityTypedata.TaskPriorityTypeId = entityKey;
                        var result = TaskPriorityTypeDataManager.GetDetails(taskPriorityTypedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				
				}
                else
                {
					taskPriorityTypedata.TaskPriorityTypeId = SetId;
                    var result = TaskPriorityTypeDataManager.GetDetails(taskPriorityTypedata, SessionVariables.RequestProfile);
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
			var data = new TaskPriorityTypeDataModel();

            PropertyMapper.CopyProperties(data, values);

            TaskPriorityTypeDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPriorityType;
            PrimaryEntityKey = "TaskPriorityType";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}