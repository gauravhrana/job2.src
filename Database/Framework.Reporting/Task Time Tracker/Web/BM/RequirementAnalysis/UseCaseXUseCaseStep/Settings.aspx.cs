using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.UseCaseXUseCaseStep
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep, "UseCaseXUseCaseStep");
        }

        override protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/Configuration/UseCaseXUseCaseStep/Default.aspx");
        }
    }
}