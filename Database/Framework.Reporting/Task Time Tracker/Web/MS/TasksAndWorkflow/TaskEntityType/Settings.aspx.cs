using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace Shared.UI.Web.TasksAndWorkflow.TaskEntityType
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.TaskEntityType, "TaskEntityType");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new TaskEntityTypeDataModel();

            data.TaskEntityTypeId = int.Parse(values[0].ToString());
            data.Name = values[1].ToString();
            data.Description = values[2].ToString();
            data.SortOrder = int.Parse(values[3].ToString());
            data.Active = int.Parse(values[4].ToString());
			Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new TaskEntityTypeDataModel();
			var dtTaskEntityType = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}