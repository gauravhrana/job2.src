using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;

namespace Shared.UI.Web.SystemIntegrity.RenumberMigration
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
	{

        #region methods

        protected override DataTable GetData()
        {
            var dt = Framework.Components.LogAndTrace.RenumberMigrationDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.AuditId);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.RenumberMigration;
            PrimaryEntityKey       = "RenumberMigration";
            PrimaryEntityIdColumn  = "RenumberMigrationId";

            MasterPageCore         = Master;
            SubMenuCore            = Master.SubMenuObject;
            BreadCrumbObject       = Master.BreadCrumbObject;

            SearchFilterCore       = oSearchFilter;
            GroupListCore          = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;

            base.OnInit(e);

        }

        #endregion

	}
}