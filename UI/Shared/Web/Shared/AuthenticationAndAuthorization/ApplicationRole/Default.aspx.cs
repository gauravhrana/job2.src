using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.UI.Web.BaseClasses;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole
{
    public partial class Default : PageDefault
    {
        protected override DataTable GetData()
        {
            var dt = ApplicationRoleDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ApplicationRole;
            PrimaryEntityKey = "ApplicationRole";
            PrimaryEntityIdColumn = "ApplicationRoleId";

            MasterPageCore = Master;
            SubMenuCore = Master.SubMenuObject;
            BreadCrumbObject = Master.BreadCrumbObject;

            SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);

            GroupListCore = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }
    }
}