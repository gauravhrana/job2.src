using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

        #region Methods

        protected override DataTable GetData()
        {
            var dt = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.Search(oSearchFilter.SearchParameters,SessionVariables.RequestProfile);

            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntity		  = Framework.Components.DataAccess.SystemEntity.ScheduleItem;
            PrimaryEntityKey      = "ScheduleItem";
            PrimaryEntityIdColumn = "ScheduleItemId";

            MasterPageCore        = Master;
            SubMenuCore           = Master.SubMenuObject;
            BreadCrumbObject      = Master.BreadCrumbObject;

            SearchFilterCore      = oSearchFilter;
            GroupListCore         = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }

        #endregion

    }
}