using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Shared.UI.Web.Controls;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityArea
{
    public partial class Default : PageDefault
    {        
        protected override DataTable GetData()
        {
            var dt = ProductivityAreaDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			 = Framework.Components.DataAccess.SystemEntity.ProductivityArea;
            PrimaryEntityKey		 = "ProductivityArea";
            PrimaryEntityIdColumn	 = "ProductivityAreaId";

            MasterPageCore			 = Master;
            SubMenuCore				 = Master.SubMenuObject;
            BreadCrumbObject		 = Master.BreadCrumbObject;

            SearchFilterCore		 = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);	
            GroupListCore			 = oGroupList;

            IsDynamicSearchControl	 = true;

            VisibilityManagerCore	 = oVC;
        }  

    }
}