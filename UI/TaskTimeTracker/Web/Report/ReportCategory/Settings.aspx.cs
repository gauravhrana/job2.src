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

namespace ApplicationContainer.UI.Web.ReportCategory
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.ReportCategory, "ReportCategory");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new ReportCategoryDataModel();

            data.ReportCategoryId    = int.Parse(values[0].ToString());
            data.Name                = values[1].ToString();           
            data.Description         = values[2].ToString();
            data.SortOrder           = int.Parse(values[3].ToString());

			Framework.Components.Core.ReportCategoryDataManager.Update(data, SessionVariables.RequestProfile);
        }
    }
}