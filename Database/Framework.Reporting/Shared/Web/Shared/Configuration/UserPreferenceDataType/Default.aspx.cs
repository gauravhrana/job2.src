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
using Framework.Components.UserPreference;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.UserPreferenceDataType
{
    public partial class Default : PageDefault
    {

        #region private methods

        protected override DataTable GetData()
        {
			var dt = UserPreferenceDataTypeDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

            return dt;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.UserPreferenceDataType;
            PrimaryEntityKey = "UserPreferenceDataType";
            PrimaryEntityIdColumn = "UserPreferenceDataTypeId";

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