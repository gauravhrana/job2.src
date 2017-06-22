using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.UI.Web.BaseClasses;
using Framework.Components.Core;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipDatabase
{
	public partial class Default : PageDefault
	{
		#region private methods

		protected override DataTable GetData()
		{
			var dt = SystemForeignRelationshipDatabaseDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			 = SystemEntity.SystemForeignRelationshipDatabase;
			PrimaryEntityKey		 = "SystemForeignRelationshipDatabase";
			PrimaryEntityIdColumn    = "SystemForeignRelationshipDatabaseId";

			MasterPageCore			 = Master;
			SubMenuCore				 = Master.SubMenuObject;
			BreadCrumbObject		 = Master.BreadCrumbObject;

			SearchFilterCore		 = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);						
			GroupListCore			 = oGroupList;

            IsDynamicSearchControl	 = true;

			VisibilityManagerCore	 = oVC;
		}

		#endregion
	}
}