﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithm
{
	public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.TaskAlgorithm, "TaskAlgorithm");
		}

		private void UpdateData(ArrayList values)
		{
			var data = new TaskAlgorithmDataModel();

			data.TaskAlgorithmId = int.Parse(values[0].ToString());
			data.Name = values[1].ToString();
			data.Description = values[2].ToString();
			data.SortOrder = int.Parse(values[3].ToString());
            TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.Update(data,SessionVariables.RequestProfile);
			ReBindEditableGrid();
		}

		private void ReBindEditableGrid()
		{
			var data = new TaskAlgorithmDataModel();
			var dtTaskAlgorithm = TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.Search(data, SessionVariables.RequestProfile);

		}
	}
}