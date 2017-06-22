using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem
{
	public partial class Settings : PageSettings
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			eSettingsList.SetUp((int)SystemEntity.TaskAlgorithmItem, "TaskAlgorithmItem");
		}

		private void UpdateData(ArrayList values)
		{
			var data = new TaskAlgorithmItemDataModel();

			data.TaskAlgorithmItemId = int.Parse(values[0].ToString());
			data.ActivityId = int.Parse(values[1].ToString());			
			data.Description = values[2].ToString();
			data.SortOrder = int.Parse(values[3].ToString());
			data.TaskAlgorithmId = int.Parse(values[4].ToString());
            TaskAlgorithmItemDataManager.Update(data, SessionVariables.RequestProfile);
			ReBindEditableGrid();
		}

		private void ReBindEditableGrid()
		{
			var data = new TaskAlgorithmItemDataModel();
			var dtTaskAlgorithmItem = TaskAlgorithmItemDataManager.Search(data, SessionVariables.RequestProfile);

		}
	}
}