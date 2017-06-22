using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using ApplicationContainer.UI.Web.CommonCode;
using Shared.UI.Web.Controls;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer.Feature;

namespace ApplicationContainer.UI.Web.RunTimeFeature
{
   public partial class Default : PageDefault
    {        
        protected override DataTable GetData()
        {
            var dt = RunTimeFeatureDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.RunTimeFeature;
            PrimaryEntityKey        = "RunTimeFeature";
            PrimaryEntityIdColumn   = "RunTimeFeatureId";

            MasterPageCore          = Master;
            SubMenuCore             = Master.SubMenuObject;
            BreadCrumbObject        = Master.BreadCrumbObject;

            SearchFilterCore        = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);	
            GroupListCore           = oGroupList;

            IsDynamicSearchControl  = true;

            VisibilityManagerCore = oVC;
        }      

    }
}