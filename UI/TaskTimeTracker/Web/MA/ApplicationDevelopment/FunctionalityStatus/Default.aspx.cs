using System;
using System.Data;
using Dapper;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityStatus
{
	public partial class Default : PageDefault
	{    
		protected override DataTable GetData()
		{
			var dt = FunctionalityStatusDataManager.GetEntityDetails(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt.ToDataTable();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity           = SystemEntity.FunctionalityStatus;
			PrimaryEntityKey		= "FunctionalityStatus";
			PrimaryEntityIdColumn	= "FunctionalityStatusId";

			MasterPageCore			= Master;      
			SubMenuCore				= Master.SubMenuObject;
			BreadCrumbObject		= Master.BreadCrumbObject;

			SearchFilterCore		= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);	

			GroupListCore			= oGroupList;

			IsDynamicSearchControl	= true;

			VisibilityManagerCore = oVC;  
		}    
	}
}

