using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.WBS.TaskXActivityInstance
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new TaskXActivityInstanceDataModel();
			UpdatedData = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TaskXActivityInstanceId =
					Convert.ToInt32(SelectedData.Rows[i][TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.TaskId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskXActivityInstanceDataModel.DataColumns.TaskId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskXActivityInstanceDataModel.DataColumns.TaskId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskXActivityInstanceDataModel.DataColumns.TaskId].ToString());

				data.ActivityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TaskXActivityInstanceDataModel.DataColumns.ActivityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(TaskXActivityInstanceDataModel.DataColumns.ActivityId).ToString())
					: int.Parse(SelectedData.Rows[i][TaskXActivityInstanceDataModel.DataColumns.ActivityId].ToString());

				TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TaskXActivityInstanceDataModel();
				data.TaskXActivityInstanceId = Convert.ToInt32(SelectedData.Rows[i][TaskXActivityInstanceDataModel.DataColumns.TaskXActivityInstanceId].ToString());
				var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var TaskXActivityInstancedata = new TaskXActivityInstanceDataModel();
			TaskXActivityInstancedata.TaskXActivityInstanceId = entityKey;
			var results = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Search(TaskXActivityInstancedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore		= DynamicUpdatePanel;
			PrimaryEntity				= Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance;
			PrimaryEntityKey			= "TaskXActivityInstance";
			BreadCrumbObject			= Master.BreadCrumbObject;
		}

		#endregion
	}
}