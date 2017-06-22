using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;


namespace Shared.UI.Web.Configuration.FieldConfiguration
{
    public partial class Default : PageDefault
	{
		
        #region private methods

        protected override System.Data.DataTable GetData()
        {
			var dt = FieldConfigurationDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.FieldConfiguration;
			PrimaryEntityKey        = "FieldConfiguration";
			PrimaryEntityIdColumn   = "FieldConfigurationId";

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