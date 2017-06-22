using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.SystemIntegrity.SearchKeyDetailItem
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
	{
		
        #region private methods

		protected override DataTable GetData()
		{
			var dt = Framework.Components.Core.SearchKeyDetailItemDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKeyDetailItem;
			PrimaryEntityKey = "SearchKeyDetailItem";
			PrimaryEntityIdColumn = "SearchKeyDetailItemId";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter;
			GroupListCore = oGroupList;

            IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
		}

		#endregion

	}
}