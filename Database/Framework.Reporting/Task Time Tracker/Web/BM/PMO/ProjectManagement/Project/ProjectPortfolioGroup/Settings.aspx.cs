using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ProjectPortfolioGroup
{
    public partial class Settings : PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)SystemEntity.ProjectPortfolioGroup, "ProjectPortfolioGroup");
        }

        override protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/Configuration/ProjectPortfolioGroup/Default.aspx");
        }
    }
}