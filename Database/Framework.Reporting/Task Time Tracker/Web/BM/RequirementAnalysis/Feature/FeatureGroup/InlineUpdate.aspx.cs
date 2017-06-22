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

namespace ApplicationContainer.UI.Web.FeatureGroup
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
                var featureGroupdata = new FeatureGroupDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetDetails(featureGroupdata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        featureGroupdata.FeatureGroupId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetDetails(featureGroupdata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    featureGroupdata.FeatureGroupId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetDetails(featureGroupdata, SessionVariables.RequestProfile);
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
            var data = new FeatureGroupDataModel();

            if (values.ContainsKey(FeatureGroupDataModel.DataColumns.FeatureGroupId))
            {
                data.FeatureGroupId = int.Parse(values[FeatureGroupDataModel.DataColumns.FeatureGroupId].ToString());
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

            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureGroup;
            PrimaryEntityKey = "FeatureGroup";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}