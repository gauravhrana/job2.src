using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.MilestoneFeatureState
{
    public partial class Settings : PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)SystemEntity.MilestoneFeatureState, "MilestoneFeatureState");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new MilestoneFeatureStateDataModel();

            data.MilestoneFeatureStateId = int.Parse(values[0].ToString());
            data.Name = values[1].ToString();
            data.Description = values[2].ToString();
            data.SortOrder = int.Parse(values[3].ToString());
            MilestoneFeatureStateDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new MilestoneFeatureStateDataModel();
            var dtMilestoneFeatureState = MilestoneFeatureStateDataManager.Search(data, SessionVariables.RequestProfile);
        }
    }
}