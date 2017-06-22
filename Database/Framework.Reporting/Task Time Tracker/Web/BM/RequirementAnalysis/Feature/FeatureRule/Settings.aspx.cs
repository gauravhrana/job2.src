using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.FeatureRule
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.FeatureRule, "FeatureRule");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new FeatureRuleDataModel();

            data.FeatureRuleId       = int.Parse(values[0].ToString());
            data.Name                = values[1].ToString();
            data.Description         = values[2].ToString();
            data.SortOrder           = int.Parse(values[3].ToString());
            data.FeatureRuleCategoryId = int.Parse(values[4].ToString());
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new FeatureRuleDataModel();
            var dtFeatureRule = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Search(data, SessionVariables.RequestProfile);
        }
    }
}