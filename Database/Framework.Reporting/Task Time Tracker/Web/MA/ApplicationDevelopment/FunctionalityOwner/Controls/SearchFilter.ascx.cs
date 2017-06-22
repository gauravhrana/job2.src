using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityOwner.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        public FunctionalityOwnerDataModel SearchParameters 
        {
            get
            {
                var data  = new FunctionalityOwnerDataModel();

                SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

                return data;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl                      = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }

    }
}