using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Module.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public ModuleDataModel SearchParameters
        {
            get
            {
                var data = new ModuleDataModel();

                data.ApplicationId = SearchFilterControl.GetParameterValueAsInt(BaseDataModel.BaseDataColumns.ApplicationId);

                SearchFilterControl.SetSearchParameters(data);

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