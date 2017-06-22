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
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.QuestionCategory
{
    public partial class Default : PageDefault
    {
        #region private methods

        protected override DataTable GetData()
        {
            var dt = QuestionCategoryDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= SystemEntity.QuestionCategory;
            PrimaryEntityKey        = "QuestionCategory";
            PrimaryEntityIdColumn   = "QuestionCategoryId";

			MasterPageCore			= Master;
			SubMenuCore				= Master.SubMenuObject;
			BreadCrumbObject		= Master.BreadCrumbObject;

			SearchFilterCore		= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);

			GroupListCore			= oGroupList;

			IsDynamicSearchControl	= true;

			VisibilityManagerCore	= oVC;                        
        }

        #endregion

    }
}