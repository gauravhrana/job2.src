using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.ReleaseLog;
using DataModel.Framework.ReleaseLog;
using System.Data;
using System.Text;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType

{
	public partial class Default : PageDefault
	{
		protected override DataTable GetData()
		{
			var dt = ReleaseIssueTypeDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			 = Framework.Components.DataAccess.SystemEntity.ReleaseIssueType;
			PrimaryEntityKey		 = "ReleaseIssueType";
			PrimaryEntityIdColumn	 = "ReleaseIssueTypeId";

			MasterPageCore			 = Master;
			SubMenuCore				 = Master.SubMenuObject;
			BreadCrumbObject		 = Master.BreadCrumbObject;

			SearchFilterCore		 = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore			 = oGroupList;

            IsDynamicSearchControl	 = true;

			VisibilityManagerCore	 = oVC;
		}       

    }
}