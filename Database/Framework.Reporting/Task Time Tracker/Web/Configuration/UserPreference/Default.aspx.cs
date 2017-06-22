using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.UserPreference
{
	public partial class Default : PageDefault
	{

		#region private methods

		protected override DataTable GetData() 
		{
			var dt = UserPreferenceDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

            if (!string.IsNullOrEmpty(oSearchFilter.CategoryLIKE))
            {
                var dv = dt.DefaultView;
                var categorylike = oSearchFilter.CategoryLIKE;
                if (!categorylike.EndsWith("%"))
                    categorylike = categorylike + "%";
                dv.RowFilter = "UserPreferenceCategory LIKE '" + categorylike + "'";
                return dv.ToTable();
            }

			return dt;
		}

		#endregion

		#region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.UserPreference;
            PrimaryEntityKey = "UserPreference";
            PrimaryEntityIdColumn = "UserPreferenceId";

            MasterPageCore = Master;
            SubMenuCore = Master.SubMenuObject;
            BreadCrumbObject = Master.BreadCrumbObject;

            SearchFilterCore = oSearchFilter;
            GroupListCore = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }

		#endregion

	}
}