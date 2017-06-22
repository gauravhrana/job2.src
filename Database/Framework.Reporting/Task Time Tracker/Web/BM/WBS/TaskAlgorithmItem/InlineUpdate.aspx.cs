using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.TaskAlgorithmItem
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
				var taskAlgorithmItemdata = new TaskAlgorithmItemDataModel();

				selectedrows = TaskAlgorithmItemDataManager.GetDetails(taskAlgorithmItemdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskAlgorithmItemdata.TaskAlgorithmItemId = entityKey;
						var result = TaskAlgorithmItemDataManager.GetDetails(taskAlgorithmItemdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					taskAlgorithmItemdata.TaskAlgorithmItemId = SetId;
					var result = TaskAlgorithmItemDataManager.GetDetails(taskAlgorithmItemdata, SessionVariables.RequestProfile);
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
			var data = new TaskAlgorithmItemDataModel();

            PropertyMapper.CopyProperties(data, values);

			TaskAlgorithmItemDataManager.Update(data,SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = SystemEntity.TaskAlgorithmItem;
            PrimaryEntityKey = "TaskAlgorithmItem";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}