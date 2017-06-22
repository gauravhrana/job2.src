using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.Competency
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.Competency, "Competency");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new CompetencyDataModel();

            data.CompetencyId    = int.Parse(values[0].ToString());
            data.Name            = values[1].ToString();
            data.Description     = values[2].ToString();
            data.SortOrder       = int.Parse(values[3].ToString());
            CompetencyDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new CompetencyDataModel();
            var dtCompetency = CompetencyDataManager.Search(data, SessionVariables.RequestProfile);           
        }
    }
}