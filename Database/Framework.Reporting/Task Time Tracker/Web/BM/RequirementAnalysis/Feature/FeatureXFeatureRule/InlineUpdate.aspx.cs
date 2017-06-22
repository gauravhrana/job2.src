using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.FeatureXFeatureRule
{
	public partial class InlineUpdate : PageInlineUpdate
	{
		#region Methods

		protected override DataTable GetData()
        {
            try
            {
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

                var selectedrows = new DataTable();
                var featureXFeatureRuledata = new FeatureXFeatureRuleDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.GetDetails(featureXFeatureRuledata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						featureXFeatureRuledata.FeatureXFeatureRuleId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.GetDetails(featureXFeatureRuledata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
                }
				else 
				{
					featureXFeatureRuledata.FeatureXFeatureRuleId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.GetDetails(featureXFeatureRuledata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);
                }
                return selectedrows;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

		protected override void Update(Dictionary<string, string> values)
        {
            var data = new FeatureXFeatureRuleDataModel();

            if (values.ContainsKey(FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId))
            {
                data.FeatureXFeatureRuleId = int.Parse(values[FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId].ToString());
            }

            if (values.ContainsKey(FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId))
            {
                data.FeatureRuleId = int.Parse(values[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId].ToString());
            }

            if (values.ContainsKey(FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId))
            {
                data.FeatureRuleStatusId = int.Parse(values[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId].ToString());
            }

            if (values.ContainsKey(FeatureXFeatureRuleDataModel.DataColumns.FeatureId))
            {
                data.FeatureId = int.Parse(values[FeatureXFeatureRuleDataModel.DataColumns.FeatureId].ToString());
            }

            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Update(data, SessionVariables.RequestProfile);
            InlineEditingList.Data = GetData();
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule;
            PrimaryEntityKey = "FeatureXFeatureRule";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}