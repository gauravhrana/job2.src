using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using System.Data;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Admin.Audit.AuditHistory
{
    public partial class Default : PageDefault
    {

		//#region properties

		//private bool isSearch
		//{
		//	get
		//	{
		//		if (ViewState["IsSearch"] == null)
		//			return false;
		//		else
		//			return Convert.ToBoolean(ViewState["IsSearch"]);
		//	}
		//	set
		//	{
		//		ViewState["IsSearch"] = value;
		//	}
		//}

		//#endregion

        #region private methods

        protected override DataTable GetData()
        {
            //DataModel.Framework.Audit.AuditHistory data = oSearchFilter.SearchParameters;

			//if (!isSearch)
			//{
			//	data.SystemEntityId = -1;
			//}

		//	var dt = Framework.Components.Audit.AuditActionDataManager.Search(oSearchFilter.SearchParameters, AuditId);
			var dt = Framework.Components.Audit.AuditHistoryDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity         = SystemEntity.AuditHistory;
            PrimaryEntityKey      = "AuditHistory";
            PrimaryEntityIdColumn = "AuditHistoryId";

			MasterPageCore		  = Master;
			SubMenuCore			  = Master.SubMenuObject;
			BreadCrumbObject      = Master.BreadCrumbObject;

			SearchFilterCore      = oSearchFilter;
			GroupListCore         = oGroupList;

			IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
        }

        #endregion

    }
}