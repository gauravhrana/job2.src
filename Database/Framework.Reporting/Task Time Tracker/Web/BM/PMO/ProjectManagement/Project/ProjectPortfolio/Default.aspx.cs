using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectPortfolio
{
    public partial class Default : PageDefault
    {        
        protected override DataTable GetData()
        {
            var dt = ProjectPortfolioDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }        

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ProjectPortfolio;
            PrimaryEntityKey        = "ProjectPortfolio";
            PrimaryEntityIdColumn   = "ProjectPortfolioId";

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