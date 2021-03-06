﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.Activity
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {
        #region private methods

        protected override DataTable GetData()
        {
			var dt = ActivityDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.Activity;
            PrimaryEntityKey		= "Activity";
            PrimaryEntityIdColumn	= "ActivityId";

            MasterPageCore			= Master;
            SubMenuCore				= Master.SubMenuObject;
            BreadCrumbObject		= Master.BreadCrumbObject;

            SearchFilterCore		= oSearchFilter;
            GroupListCore			= oGroupList;

            IsDynamicSearchControl  = true;

            VisibilityManagerCore	= oVC;
        }

        #endregion

    }
}