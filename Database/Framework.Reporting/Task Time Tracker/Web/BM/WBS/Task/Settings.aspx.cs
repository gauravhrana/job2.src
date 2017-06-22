using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace ApplicationContainer.UI.Web.WBS.Task
{
    public partial class Settings : PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)SystemEntity.Task, "Task");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new TaskDataModel();

            data.TaskId = int.Parse(values[0].ToString());
            data.TaskTypeId = int.Parse(values[1].ToString());
            data.Name = values[2].ToString();
            data.Description = values[3].ToString();
            data.SortOrder = int.Parse(values[4].ToString());

            TaskDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new TaskDataModel();
            var dtTask = TaskDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}