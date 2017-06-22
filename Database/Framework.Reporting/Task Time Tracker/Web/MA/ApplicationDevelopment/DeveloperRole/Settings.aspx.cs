using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.DeveloperRole
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.DeveloperRole, "DeveloperRole");
        }

        private void UpdateData(ArrayList values)
        { 
            var data = new DeveloperRoleDataModel();

            data.DeveloperRoleId    = int.Parse(values[0].ToString());
            data.Name               = values[1].ToString();
            data.Description        = values[2].ToString();
            data.SortOrder          = int.Parse(values[3].ToString());
            TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new DeveloperRoleDataModel();
			var dtDeveloperRole = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.Search(data, SessionVariables.RequestProfile);
        }
    }
}