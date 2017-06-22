using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Feature.FeatureRuleCategory
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
				var featureRuleCategorydata = new FeatureRuleCategoryDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetDetails(featureRuleCategorydata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						featureRuleCategorydata.FeatureRuleCategoryId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetDetails(featureRuleCategorydata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{					
					featureRuleCategorydata.FeatureRuleCategoryId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetDetails(featureRuleCategorydata, SessionVariables.RequestProfile);
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
			var data = new FeatureRuleCategoryDataModel();

            if (values.ContainsKey(FeatureRuleCategoryDataModel.DataColumns.FeatureRuleCategoryId))
            {
                data.FeatureRuleCategoryId = int.Parse(values[FeatureRuleCategoryDataModel.DataColumns.FeatureRuleCategoryId].ToString());
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Name))
            {
                data.Name = values[StandardDataModel.StandardDataColumns.Name].ToString();
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Description))
            {
                data.Description = values[StandardDataModel.StandardDataColumns.Description].ToString();
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.SortOrder))
            {
                data.SortOrder = int.Parse(values[StandardDataModel.StandardDataColumns.SortOrder].ToString());
            }

            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.Update(data, SessionVariables.RequestProfile);			
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleCategory;
            PrimaryEntityKey = "FeatureRuleCategory";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}