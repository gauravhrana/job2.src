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
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage
{
    public partial class Default : PageDefault
    {
        
        #region private methods

        protected override DataTable GetData()
        {
			var dt = FunctionalityImageDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

            return dt;
        }

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.FunctionalityImage;
			PrimaryEntityKey		= "FunctionalityImage";
			PrimaryEntityIdColumn	= "FunctionalityImageId";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore = oGroupList;

            IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
		}

        #endregion

    }
}