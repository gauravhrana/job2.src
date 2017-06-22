using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {
        
        #region private methods

        protected override DataTable GetData()
        {
            var dt = ActivityXDeliverableArtifactDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ActivityXDeliverableArtifact;
			PrimaryEntityKey = "ActivityXDeliverableArtifact";
			PrimaryEntityIdColumn = "ActivityXDeliverableArtifactId";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter;
			GroupListCore = oGroupList;

			VisibilityManagerCore = oVC;
		}
		       
        #endregion

    }
}