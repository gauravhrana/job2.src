using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using System.Data;
using Shared.UI.Web.Controls;
using DataModel.Framework.DataAccess;
using TestCaseManagement.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TCM.TestCaseStatus
{
	public partial class Default : PageDefault
	{
		protected override DataTable GetData()
		{
            var dt = TestCaseStatusDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity				 = Framework.Components.DataAccess.SystemEntity.TestCaseStatus;
			PrimaryEntityKey			 = "TestCaseStatus";
			PrimaryEntityIdColumn		 = "TestCaseStatusId";

			MasterPageCore				= Master;
			SubMenuCore					= Master.SubMenuObject;
			BreadCrumbObject			= Master.BreadCrumbObject;

			SearchFilterCore			= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore				= oGroupList;

			IsDynamicSearchControl		= true;

			VisibilityManagerCore		= oVC;
		}

	}
}