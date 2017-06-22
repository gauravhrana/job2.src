using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TaskAlgorithm
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
				var taskAlgorithmdata = new TaskAlgorithmDataModel();

				selectedrows = TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.GetDetails(taskAlgorithmdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskAlgorithmdata.TaskAlgorithmId = entityKey;
						var result = TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.GetDetails(taskAlgorithmdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					taskAlgorithmdata.TaskAlgorithmId = SetId;
					var result = TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.GetDetails(taskAlgorithmdata, SessionVariables.RequestProfile);
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
			var data = new TaskAlgorithmDataModel();

            PropertyMapper.CopyProperties(data, values);

			TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskAlgorithm;
            PrimaryEntityKey = "TaskAlgorithm";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}