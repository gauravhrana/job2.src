using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FeatureOwnerStatus
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.FeatureOwnerStatus, "FeatureOwnerStatus");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new FeatureOwnerStatusDataModel();

            data.FeatureOwnerStatusId    = int.Parse(values[0].ToString());
            data.Name                    = values[1].ToString();
            data.Description             = values[2].ToString();
            data.SortOrder               = int.Parse(values[3].ToString());
            TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.Update(data, SessionVariables.RequestProfile);
        }
    }
}