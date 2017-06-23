using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.Report
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.Report, "Report");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new ReportDataModel();

            data.ReportId        = int.Parse(values[0].ToString());
            data.Name            = values[1].ToString();
            data.Title           = values[2].ToString();
            data.Description     = values[3].ToString();
            data.SortOrder       = int.Parse(values[4].ToString());

			Framework.Components.Core.ReportDataManager.Update(data, SessionVariables.RequestProfile);
        }
    }
}