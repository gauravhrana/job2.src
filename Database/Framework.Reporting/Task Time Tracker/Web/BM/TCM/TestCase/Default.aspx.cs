using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.TCM.TestCase
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

        #region private methods

        protected override DataTable GetData()
        {
            var dt = TestCaseManagement.Components.DataAccess.TestCaseDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

            return dt;
        }

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCase;
			PrimaryEntityKey         = "TestCase";
			PrimaryEntityIdColumn    = "TestCaseId";

			MasterPageCore           = Master;
			SubMenuCore              = Master.SubMenuObject;
			BreadCrumbObject         = Master.BreadCrumbObject;

            SearchFilterCore         = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
			GroupListCore            = oGroupList;

            IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
		}

		#endregion

    }
}