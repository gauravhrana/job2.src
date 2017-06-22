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
using System.Text;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityStatus.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        public FunctionalityStatusDataModel SearchParameters
        {
            get
            {
                var data = new FunctionalityStatusDataModel();

                SearchFilterControl.SetSearchParameters(data);

                data.ApplicationId = SearchFilterControl.GetParameterValueAsInt(BaseDataModel.BaseDataColumns.ApplicationId);

				CommonSearchParameters();

                return data;
            }
        }


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