using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.FeatureXFeatureRule.Controls

{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        public FeatureXFeatureRuleDataModel SearchParameters
        {
            get
            {
                var data = new FeatureXFeatureRuleDataModel();

                SearchFilterControl.SetSearchParameters(data);

                CommonSearchParameters();

                return data;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BaseSearchFilterControl = SearchFilterControl;
            SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }

    }
}