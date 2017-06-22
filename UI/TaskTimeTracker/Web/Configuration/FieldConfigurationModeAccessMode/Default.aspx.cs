using System;
using System.Data;
using Dapper;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.UserPreference;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeAccessMode
{
	public partial class Default : PageDefault
	{    
		protected override DataTable GetData()
		{
			var dt = FieldConfigurationModeAccessModeDataManager.GetEntityDetails(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt.ToDataTable();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity           = SystemEntity.FieldConfigurationModeAccessMode;
			PrimaryEntityKey		= "FieldConfigurationModeAccessMode";
			PrimaryEntityIdColumn	= "FieldConfigurationModeAccessModeId";

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

