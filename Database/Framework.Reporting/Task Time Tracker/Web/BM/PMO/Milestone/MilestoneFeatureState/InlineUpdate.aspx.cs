﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.MilestoneFeatureState
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
				var milestoneFeatureStatedata = new MilestoneFeatureStateDataModel();

                selectedrows = MilestoneFeatureStateDataManager.GetDetails(milestoneFeatureStatedata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						milestoneFeatureStatedata.MilestoneFeatureStateId = entityKey;
                        var result = MilestoneFeatureStateDataManager.GetDetails(milestoneFeatureStatedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
                }
				else
				{
					milestoneFeatureStatedata.MilestoneFeatureStateId = SetId;
                    var result = MilestoneFeatureStateDataManager.GetDetails(milestoneFeatureStatedata, SessionVariables.RequestProfile);
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
			var data = new MilestoneFeatureStateDataModel();

            if (values.ContainsKey(MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId))
            {
                data.MilestoneFeatureStateId = int.Parse(values[MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId]);
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Name))
            {
                data.Name = values[StandardDataModel.StandardDataColumns.Name];
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Description))
            {
                data.Description = values[StandardDataModel.StandardDataColumns.Description];
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.SortOrder))
            {
                data.SortOrder = int.Parse(values[StandardDataModel.StandardDataColumns.SortOrder]);
            }

            MilestoneFeatureStateDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events
		
		protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = SystemEntity.MilestoneFeatureState;
            PrimaryEntityKey = "MilestoneFeatureState";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}