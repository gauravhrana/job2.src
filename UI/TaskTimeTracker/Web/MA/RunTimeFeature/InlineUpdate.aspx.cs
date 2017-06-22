using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.Feature.RunTimeFeature
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

				var selectedrows = new List<RunTimeFeatureDataModel>();
                var runTimeFeaturedata = new RunTimeFeatureDataModel();

                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        runTimeFeaturedata.RunTimeFeatureId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.GetDetails(runTimeFeaturedata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
                    }
                }
                else if (SetId != 0)
                {
                    runTimeFeaturedata.RunTimeFeatureId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.GetDetails(runTimeFeaturedata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);
                }
                return selectedrows.ToDataTable();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return null;
        }

        protected override void Update(Dictionary<string, string> values)
        {
            var data = new RunTimeFeatureDataModel();

            if (values.ContainsKey(RunTimeFeatureDataModel.DataColumns.RunTimeFeatureId))
            {
                data.RunTimeFeatureId = int.Parse(values[RunTimeFeatureDataModel.DataColumns.RunTimeFeatureId].ToString());
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

            TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.Update(data, SessionVariables.RequestProfile);

            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.RunTimeFeature;
            PrimaryEntityKey = "RunTimeFeature";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}