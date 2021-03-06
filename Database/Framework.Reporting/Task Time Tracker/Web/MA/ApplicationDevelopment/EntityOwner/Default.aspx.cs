﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner
{
    public partial class Default : PageDefault
    {

        #region private methods

        protected override DataTable GetData()
        {
			var dt = EntityOwnerDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity          = SystemEntity.EntityOwner;
            PrimaryEntityKey       = "EntityOwner";
            PrimaryEntityIdColumn  = "EntityOwnerId";

            MasterPageCore         = Master;
            SubMenuCore            = Master.SubMenuObject;
            BreadCrumbObject       = Master.BreadCrumbObject;

			SearchFilterCore	   = oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
            GroupListCore          = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore  = oVC;
        }

        #endregion

    }
}