using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Feature
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
                var featuredata = new FeatureDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetDetails(featuredata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        featuredata.FeatureId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetDetails(featuredata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    featuredata.FeatureId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetDetails(featuredata, SessionVariables.RequestProfile);
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
			var data = new FeatureDataModel();

            if (values.ContainsKey(FeatureDataModel.DataColumns.FeatureId))
            {
                data.FeatureId = int.Parse(values[FeatureDataModel.DataColumns.FeatureId].ToString());
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

            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Feature;
            PrimaryEntityKey = "Feature";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}