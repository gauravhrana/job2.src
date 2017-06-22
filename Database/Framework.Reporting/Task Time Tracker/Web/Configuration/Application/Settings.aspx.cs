﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace Shared.UI.Web.Configuration.Application
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.Application, "Application");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new ApplicationDataModel();

            data.ApplicationId       = int.Parse(values[0].ToString());
            data.Name                = values[1].ToString();
            data.Description         = values[2].ToString();
            data.SortOrder           = int.Parse(values[3].ToString());
			Framework.Components.ApplicationUser.ApplicationDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new ApplicationDataModel();
			var dtApplication = Framework.Components.ApplicationUser.ApplicationDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}