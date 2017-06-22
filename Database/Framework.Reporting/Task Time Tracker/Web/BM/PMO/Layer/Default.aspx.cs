using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using TaskTimeTracker.Components.BusinessLayer;


namespace ApplicationContainer.UI.Web.Layer
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {
        protected override DataTable GetData()
        {
            var dt = LayerDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.Layer;
            PrimaryEntityKey        = "Layer";
            PrimaryEntityIdColumn   = "LayerId";

            MasterPageCore          = Master;
            SubMenuCore             = Master.SubMenuObject;
            BreadCrumbObject        = Master.BreadCrumbObject;

            SearchFilterCore        = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);						
            GroupListCore           = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }        
    }
}