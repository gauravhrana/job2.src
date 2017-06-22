using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace Shared.UI.Web.Configuration.UserPreferenceSelectedItem
{
	public partial class Default : PageDefault
	{

		#region private methods

		protected override DataTable GetData() 
		{
			var dt = UserPreferenceSelectedItemDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

			return dt;
		}

		#endregion

		#region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= SystemEntity.UserPreferenceSelectedItem;
            PrimaryEntityKey        = "UserPreferenceSelectedItem";
            PrimaryEntityIdColumn   = "UserPreferenceSelectedItemId";

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