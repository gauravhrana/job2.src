using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.MA.ApplicationDevelopment.Customer
{
	public partial class Default : PageDefault
	{

		protected override DataTable GetData()
		{
			var dt = CustomerDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.Customer;
			PrimaryEntityKey = "Customer";
			PrimaryEntityIdColumn = "CustomerId";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey); ;
			GroupListCore = oGroupList;

			IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
		}
	}
}