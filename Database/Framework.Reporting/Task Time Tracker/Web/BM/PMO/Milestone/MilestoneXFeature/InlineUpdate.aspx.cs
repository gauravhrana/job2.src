using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.MilestoneXFeature
{
    public partial class InlineUpdate : PageInlineUpdate
    {
		#region Methods

		protected override DataTable GetData()
        {
            try
            {
				var SuperKey = ApplicationCommon.GetSuperKey();
				var SetId = ApplicationCommon.GetSetId();

				var selectedrows = new DataTable();
                var milestoneXFeaturedata = new MilestoneXFeatureDataModel();

                selectedrows = MilestoneXFeatureDataManager.GetDetails(milestoneXFeaturedata, SessionVariables.RequestProfile).Clone();

				if (!string.IsNullOrEmpty(SuperKey))
                {
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						milestoneXFeaturedata.MilestoneXFeatureId = entityKey;
                        var result = MilestoneXFeatureDataManager.GetDetails(milestoneXFeaturedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
                }
                else 
                {
					milestoneXFeaturedata.MilestoneXFeatureId = SetId;
                    var result = MilestoneXFeatureDataManager.GetDetails(milestoneXFeaturedata, SessionVariables.RequestProfile);
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
            var data = new MilestoneXFeatureDataModel();

            if (values.ContainsKey(MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId))
            {
                data.MilestoneXFeatureId = int.Parse(values[MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId].ToString());
            }

            if (values.ContainsKey(MilestoneXFeatureDataModel.DataColumns.MilestoneId))
            {
                data.MilestoneId = int.Parse(values[MilestoneXFeatureDataModel.DataColumns.MilestoneId].ToString());
            }

            if (values.ContainsKey(MilestoneXFeatureDataModel.DataColumns.FeatureId))
            {
                data.FeatureId = int.Parse(values[MilestoneXFeatureDataModel.DataColumns.FeatureId].ToString());
            }

            MilestoneXFeatureDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = SystemEntity.MilestoneXFeature;
            PrimaryEntityKey = "MilestoneXFeature";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}