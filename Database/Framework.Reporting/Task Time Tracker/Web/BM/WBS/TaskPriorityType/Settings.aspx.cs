using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Priority;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityType
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.TaskPriorityType, "TaskPriorityType");
        }

        private void UpdateData(ArrayList values)
        {
			var data = new TaskPriorityTypeDataModel();

            data.TaskPriorityTypeId      = int.Parse(values[0].ToString());
            data.Name                    = values[1].ToString();
            data.Description             = values[2].ToString();
            data.SortOrder               = int.Parse(values[3].ToString());
            TaskPriorityTypeDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
			var data = new TaskPriorityTypeDataModel();
            var dtTaskPriorityType = TaskPriorityTypeDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}