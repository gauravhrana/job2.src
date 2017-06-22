using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

        #region Methods

        protected override DataTable GetData()
        {
            var dt = ScheduleQuestionDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;
            PrimaryEntityKey        = "ScheduleQuestion";
            PrimaryEntityIdColumn   = "ScheduleQuestionId";

            MasterPageCore          = Master;
            SubMenuCore             = Master.SubMenuObject;
            BreadCrumbObject        = Master.BreadCrumbObject;

            SearchFilterCore        = oSearchFilter;
            GroupListCore           = oGroupList;

			IsDynamicSearchControl  = true;
			 
            VisibilityManagerCore = oVC;
        }

        #endregion

    }
}