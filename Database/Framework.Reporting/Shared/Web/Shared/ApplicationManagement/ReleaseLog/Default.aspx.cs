using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.ReleaseLog;
using DataModel.Framework.ReleaseLog;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog
{
 
	public partial class Default : PageDefault
	{
		protected override DataTable GetData()
		{
			var dt = ReleaseLogDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.AuditId);
			return dt;
		}
		       
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.ReleaseLog;
            PrimaryEntityKey      = "ReleaseLog";
            PrimaryEntityIdColumn = "ReleaseLogId";

            MasterPageCore        = Master;
            SubMenuCore           = Master.SubMenuObject;
            BreadCrumbObject      = Master.BreadCrumbObject;

			SearchFilterCore	  = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
            GroupListCore         = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore  = oVC;
        }        

    }
}