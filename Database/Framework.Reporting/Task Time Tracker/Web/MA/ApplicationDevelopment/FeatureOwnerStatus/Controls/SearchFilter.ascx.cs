using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.UserPreference;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FeatureOwnerStatus.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public FeatureOwnerStatusDataModel SearchParameters
        {
            get
            {
                var data = new FeatureOwnerStatusDataModel();

                SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

                return data;
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }

        #endregion

    }
}