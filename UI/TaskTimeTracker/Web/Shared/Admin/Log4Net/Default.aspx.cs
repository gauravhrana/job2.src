using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.Log4Net
{
	public partial class Default : PageDefault
    {

        #region methods

        protected DataTable GetData(int pageIndex, int pageSize, string orderBy, string orderByDirection)
        {
            var data = new Framework.Components.LogAndTrace.Log4NetDataModel();
            var dt = Framework.Components.LogAndTrace.Log4NetDataManager.SearchWithPaging(data, pageIndex, pageSize, orderBy, orderByDirection, SessionVariables.RequestProfile);
            return dt;
        }		

		protected override DataTable GetData()
		{
			var dt = Framework.Components.LogAndTrace.Log4NetDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}
       
        #endregion

        #region Events
        
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Log4Net;
			PrimaryEntityKey = "Log4Net";
			PrimaryEntityIdColumn = "Id";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore = oGroupList;

			IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;

			SettingCategory = "Log4NetDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}       

        protected void dgvLog4Net_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        #endregion

    }
}