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
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCaseWorkFlowCategory
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.UseCaseWorkFlowCategory, "UseCaseWorkFlowCategory");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new UseCaseWorkFlowCategoryDataModel();

            data.UseCaseWorkFlowCategoryId       = int.Parse(values[0].ToString());
            data.Name                            = values[1].ToString();
            data.Description                     = values[2].ToString();
            data.SortOrder                       = int.Parse(values[3].ToString());
            TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new UseCaseWorkFlowCategoryDataModel();
            var dtUseCaseWorkFlowCategory = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}