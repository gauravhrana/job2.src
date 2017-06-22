﻿using System;
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

namespace ApplicationContainer.UI.Web.Feature.FeatureRule
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
				var featureRuledata = new FeatureRuleDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetDetails(featureRuledata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						featureRuledata.FeatureRuleId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetDetails(featureRuledata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else if (SetId != 0)
				{
					featureRuledata.FeatureRuleId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetDetails(featureRuledata, SessionVariables.RequestProfile);
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
			var data = new FeatureRuleDataModel();

            if (values.ContainsKey(FeatureRuleDataModel.DataColumns.FeatureRuleId))
            {
                data.FeatureRuleId = int.Parse(values[FeatureRuleDataModel.DataColumns.FeatureRuleId].ToString());
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Name))
            {
                data.Name = values[StandardDataModel.StandardDataColumns.Name].ToString();
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Description))
            {
                data.Description = values[StandardDataModel.StandardDataColumns.Description].ToString();
            }

            if (values.ContainsKey(FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId))
            {
                data.FeatureRuleCategoryId = int.Parse(values[FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId].ToString());
            }
			
            if (values.ContainsKey(StandardDataModel.StandardDataColumns.SortOrder))
            {
                data.SortOrder = int.Parse(values[StandardDataModel.StandardDataColumns.SortOrder].ToString());
            }

            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Update(data, SessionVariables.RequestProfile);
			
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRule;
            PrimaryEntityKey = "FeatureRule";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}