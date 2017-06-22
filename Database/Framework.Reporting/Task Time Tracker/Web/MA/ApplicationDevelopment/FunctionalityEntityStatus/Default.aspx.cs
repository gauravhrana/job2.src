using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.Components.DataAccess;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

        #region private methods

        protected override DataTable GetData()
        {
            Log4Net.LogInfo("Step3 Get FES Search results START", "FES Search", SessionVariables.RequestProfile.ApplicationId);
            var dt = FunctionalityEntityStatusDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            if (oSearchFilter.ShowGraph)
            {
                oGroupList.Visible = false;
                FESChart1.Visible = true;
            }
            else
            {
                oGroupList.Visible = true;
                FESChart1.Visible = false;
            }
			Log4Net.LogInfo("Step3 Get FES Search results END", "FES Search", SessionVariables.RequestProfile.ApplicationId);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity           = SystemEntity.FunctionalityEntityStatus;
            PrimaryEntityKey        = "FunctionalityEntityStatus";
            PrimaryEntityIdColumn   = "FunctionalityEntityStatusId";

            MasterPageCore          = Master;
            SubMenuCore             = Master.SubMenuObject;
            BreadCrumbObject        = Master.BreadCrumbObject;

            SearchFilterCore        = oSearchFilter;
            GroupListCore           = oGroupList;

			IsDynamicSearchControl	= true;

            VisibilityManagerCore   = oVC;

            oSearchFilter.ShowGraph = bool.Parse(PerferenceUtility.GetUserPreferenceByKey("ShowGraph", "FunctionalityEntityStatusDefaultViewSearchControl"));

        }

        protected void lnkGridSummary_Click(object sender, EventArgs e)
        {
            if(lnkGridSummary.Text.Contains("Summary"))
            {
                oGroupList.Visible = false;
                FESChart1.Visible = true;
                lnkGridSummary.Text = "Show Grid";
            }
            else{
                oGroupList.Visible = true;
                FESChart1.Visible = false;
                lnkGridSummary.Text = "Show Summary";
                oSearchFilter.RaiseSearch();
            }
        }

        #endregion

    }
}