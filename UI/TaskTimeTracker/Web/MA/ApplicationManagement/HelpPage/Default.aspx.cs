using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;
using Framework.Components.Core;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationManagement.HelpPage
{
	public partial class Default : PageDefault
	{
		#region private methods

		protected override DataTable GetData()
		{
			var dt = HelpPageDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.HelpPage;
			PrimaryEntityKey		= "HelpPage";
			PrimaryEntityIdColumn	= "HelpPageId";

			MasterPageCore			= Master;
			SubMenuCore				= Master.SubMenuObject;
			BreadCrumbObject		= Master.BreadCrumbObject;

			SearchFilterCore		= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore			= oGroupList;

			VisibilityManagerCore	= oVC;
		}

		#endregion
	}
}