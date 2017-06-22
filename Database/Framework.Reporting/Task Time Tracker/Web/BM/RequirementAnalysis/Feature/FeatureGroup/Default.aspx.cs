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

namespace ApplicationContainer.UI.Web.FeatureGroup
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
	{

		#region private methods

		protected override DataTable GetData()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

			return dt;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.FeatureGroup;
			PrimaryEntityKey        = "FeatureGroup";
			PrimaryEntityIdColumn   = "FeatureGroupId";

			MasterPageCore          = Master;
			SubMenuCore             = Master.SubMenuObject;
			BreadCrumbObject        = Master.BreadCrumbObject;

			SearchFilterCore		= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);						
			GroupListCore           = oGroupList;

            IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
		}

		#endregion

	}
}