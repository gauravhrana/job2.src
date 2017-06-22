using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.Activity
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.Activity, "Activity");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new ActivityDataModel(); 

            data.ActivityId      = int.Parse(values[0].ToString());
            data.Name            = values[1].ToString();
            data.Description     = values[2].ToString();
            data.SortOrder       = int.Parse(values[3].ToString());
            data.LayerId         = int.Parse(values[4].ToString());

			ActivityDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new ActivityDataModel();
			var dtActivity = ActivityDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}