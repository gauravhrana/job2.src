using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Shared.UI.Web.Controls;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatus
{
	public partial class Default : PageDefault
	{
		protected override DataTable GetData()
		{
			var dt = ProjectUseCaseStatusDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			 = SystemEntity.ProjectUseCaseStatus;
			PrimaryEntityKey		 = "ProjectUseCaseStatus";
			PrimaryEntityIdColumn	 = "ProjectUseCaseStatusId";

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