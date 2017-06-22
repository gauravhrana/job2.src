using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.Task;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask
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
				var taskPackageXOwnerXTaskdata = new TaskPackageXOwnerXTaskDataModel();

				selectedrows = TaskPackageXOwnerXTaskDataManager.GetDetails(taskPackageXOwnerXTaskdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskPackageXOwnerXTaskdata.TaskPackageXOwnerXTaskId = entityKey;
						var result = TaskPackageXOwnerXTaskDataManager.GetDetails(taskPackageXOwnerXTaskdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					taskPackageXOwnerXTaskdata.TaskPackageXOwnerXTaskId = SetId;
					var result = TaskPackageXOwnerXTaskDataManager.GetDetails(taskPackageXOwnerXTaskdata, SessionVariables.RequestProfile);
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
			var data = new TaskPackageXOwnerXTaskDataModel();

			PropertyMapper.CopyProperties(data, values);

			TaskPackageXOwnerXTaskDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask;
			PrimaryEntityKey = "TaskPackageXOwnerXTask";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion		
	}
}