using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {
        #region private methods

        protected override DataTable GetData()
        {
			var dt = FunctionalityImageInstanceDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= SystemEntity.FunctionalityImageInstance;
            PrimaryEntityKey		= "FunctionalityImageInstance";
            PrimaryEntityIdColumn	= "FunctionalityImageInstanceId";

            MasterPageCore = Master;
            SubMenuCore = Master.SubMenuObject;
            BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
            GroupListCore = oGroupList;

            VisibilityManagerCore = oVC;
        }

        #endregion

    }
}