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
//using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Scheduling.VacationPlan
{
	public partial class Default : PageDefault
	{

		protected override DataTable GetData()
		{
            var dt = VacationPlanDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

			return dt;
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntity = SystemEntity.VacationPlan;
            PrimaryEntityKey      = "VacationPlan";
            PrimaryEntityIdColumn = "VacationPlanId";

            MasterPageCore        = Master;
            SubMenuCore           = Master.SubMenuObject;
            BreadCrumbObject      = Master.BreadCrumbObject;

            SearchFilterCore      = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
            GroupListCore         = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }

	}
}