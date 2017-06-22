using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.EventMonitoring;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.ApplicationMonitoredEventProcessingState
{
    public partial class Default : PageDefault
    {

        #region private methods

        protected override DataTable GetData()
        {
			var dt = ApplicationMonitoredEventProcessingStateDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity         = SystemEntity.ApplicationMonitoredEventProcessingState;
            PrimaryEntityKey      = "ApplicationMonitoredEventProcessingState";
            PrimaryEntityIdColumn = "ApplicationMonitoredEventProcessingStateId";

            MasterPageCore        = Master;
            SubMenuCore           = Master.SubMenuObject;
            BreadCrumbObject      = Master.BreadCrumbObject;

			SearchFilterCore	  = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
            GroupListCore         = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }   

        #endregion

    }
}