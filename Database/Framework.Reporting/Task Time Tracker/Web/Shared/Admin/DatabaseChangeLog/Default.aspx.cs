using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;

namespace Shared.UI.Web.DatabaseChangeLog
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
	{

        #region methods

        protected override DataTable GetData()
        {
            var dt = Framework.Components.LogAndTrace.DatabaseChangeLogDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

            PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.DatabaseChangeLog;
            PrimaryEntityKey      = "DatabaseChangeLog";
            PrimaryEntityIdColumn = "Id";

            MasterPageCore        = Master;
            SubMenuCore           = Master.SubMenuObject;
            BreadCrumbObject      = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore = oGroupList;

			IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;            

        }

        #endregion

	}
}