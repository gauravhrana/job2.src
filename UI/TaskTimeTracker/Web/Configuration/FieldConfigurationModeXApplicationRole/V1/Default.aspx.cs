using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {
        
        #region private methods

        protected override DataTable GetData()
        {
            var dt = FieldConfigurationModeXApplicationRoleDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;
            PrimaryEntityKey      = "FieldConfigurationModeXApplicationRole";
            PrimaryEntityIdColumn = "FieldConfigurationModeXApplicationRoleId";

            MasterPageCore        = Master;
            SubMenuCore           = Master.SubMenuObject;
            BreadCrumbObject      = Master.BreadCrumbObject;

            SearchFilterCore      = oSearchFilter;
            GroupListCore         = oGroupList;

            VisibilityManagerCore = oVC;
        }

        #endregion

    }
}