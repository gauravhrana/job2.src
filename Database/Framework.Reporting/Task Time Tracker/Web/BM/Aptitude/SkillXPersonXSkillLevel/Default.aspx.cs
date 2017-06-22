using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

        #region private methods

        protected override DataTable GetData()
        {
            var dt = SkillXPersonXSkillLevelDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntity		  = Framework.Components.DataAccess.SystemEntity.SkillXPersonXSkillLevel;
            PrimaryEntityKey      = "SkillXPersonXSkillLevel";
            PrimaryEntityIdColumn = "SkillXPersonXSkillLevelId";

            MasterPageCore        = Master;
            SubMenuCore           = Master.SubMenuObject;
            BreadCrumbObject      = Master.BreadCrumbObject;

            SearchFilterCore      = oSearchFilter;
            GroupListCore         = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore = oVC;
        }

        #endregion

    }
}