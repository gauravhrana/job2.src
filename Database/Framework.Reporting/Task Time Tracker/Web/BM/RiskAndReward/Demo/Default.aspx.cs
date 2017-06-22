using System;
using System.Data;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Demo
{
	public partial class Default : PageDefault
	{    
		protected override DataTable GetData()
		{
			var dt = RiskDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity           = SystemEntity.Risk;
			PrimaryEntityKey		= "Risk";
			PrimaryEntityIdColumn	= "RiskId";

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

