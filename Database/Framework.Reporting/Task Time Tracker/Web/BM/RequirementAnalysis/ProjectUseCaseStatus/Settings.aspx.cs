﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatus
{
	public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.ProjectUseCaseStatus, "ProjectUseCaseStatus");
		}

		private void UpdateData(ArrayList values)
		{

			var data = new ProjectUseCaseStatusDataModel();

			data.ProjectUseCaseStatusId      = int.Parse(values[0].ToString());
			data.Name                        = values[1].ToString();
			data.Description                 = values[2].ToString();
			data.SortOrder                   = int.Parse(values[3].ToString());
            TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.Update(data, SessionVariables.RequestProfile);
			ReBindEditableGrid();
		}

		private void ReBindEditableGrid()
		{
			var data = new ProjectUseCaseStatusDataModel();
            var dtProjectUseCaseStatus = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.Search(data, SessionVariables.RequestProfile);

		}
	}
}