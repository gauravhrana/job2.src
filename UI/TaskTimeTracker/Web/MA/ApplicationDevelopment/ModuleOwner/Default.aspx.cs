using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Shared.UI.Web.Controls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using System.Web.UI.HtmlControls;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner
{
	public partial class Default : PageDefault
    {
		#region private methods

		protected override DataTable GetData()
		{
			var dt = ModuleOwnerDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ModuleOwner;
			PrimaryEntityKey = "ModuleOwner";
			PrimaryEntityIdColumn = "ModuleOwnerId";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore = oGroupList;

			IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
		}

		#endregion


    }
}