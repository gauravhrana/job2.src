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

namespace ApplicationContainer.UI.Web.DeliverableArtifact
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {
        
        #region private methods

        protected override DataTable GetData()
        {
            var dt = DeliverableArtifactDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.DeliverableArtifact;
			PrimaryEntityKey        = "DeliverableArtifact";
			PrimaryEntityIdColumn   = "DeliverableArtifactId";

			MasterPageCore          = Master;
			SubMenuCore             = Master.SubMenuObject;
			BreadCrumbObject        = Master.BreadCrumbObject;

			SearchFilterCore		= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);						
			GroupListCore           = oGroupList;

            IsDynamicSearchControl  = true;

			VisibilityManagerCore = oVC;
		}

        #endregion

    }
}