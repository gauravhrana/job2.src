using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.Configuration
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionVariables.CurrentApplicationModuleCode = "AA";
            //SessionVariables.UserPreferedMenuData = MenuHelper.GetUserPreferedMenu();
        }
    }
}