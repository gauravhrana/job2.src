using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.DeliverableArtifact
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.DeliverableArtifact, "DeliverableArtifact");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new DeliverableArtifactDataModel();

            data.DeliverableArtifactId   = int.Parse(values[0].ToString());
            data.Name                    = values[1].ToString();
            data.Description             = values[2].ToString();
            data.SortOrder               = int.Parse(values[3].ToString());
            DeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new DeliverableArtifactDataModel();
            var dtDeliverableArtifact = DeliverableArtifactDataManager.Search(data, SessionVariables.RequestProfile);
        }
    }
}