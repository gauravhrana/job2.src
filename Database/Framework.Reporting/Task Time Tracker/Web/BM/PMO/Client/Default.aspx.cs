using System;
using System.Data;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.PMO.Client
{
	public partial class Default : PageDefault
    {        
        protected override DataTable GetData()
        {
            var dt = ClientDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }
        
        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			PrimaryEntity           = SystemEntity.Client;
            PrimaryEntityKey		= "Client";
            PrimaryEntityIdColumn	= "ClientId";                
                
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