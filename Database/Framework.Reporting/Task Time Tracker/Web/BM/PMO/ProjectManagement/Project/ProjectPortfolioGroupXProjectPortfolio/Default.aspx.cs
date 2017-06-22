using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
//using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectPortfolioGroupXProjectPortfolio
{
	public partial class Default : PageDefault
	{

		#region private methods

		protected override DataTable GetData()
		{
            var dt = ProjectPortfolioGroupXProjectPortfolioDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

			return dt;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.ProjectPortfolioGroupXProjectPortfolio;
			PrimaryEntityKey = "ProjectPortfolioGroupXProjectPortfolio";
			PrimaryEntityIdColumn = "ProjectPortfolioGroupXProjectPortfolioId";

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