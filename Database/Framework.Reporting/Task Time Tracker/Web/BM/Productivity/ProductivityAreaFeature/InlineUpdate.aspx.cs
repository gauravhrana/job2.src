﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaFeature
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
				var productivityAreaFeaturedata = new ProductivityAreaFeatureDataModel();

                selectedrows = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.GetDetails(productivityAreaFeaturedata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        productivityAreaFeaturedata.ProductivityAreaFeatureId = entityKey;
                        var result = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.GetDetails(productivityAreaFeaturedata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    productivityAreaFeaturedata.ProductivityAreaFeatureId = SetId;
                    var result = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.GetDetails(productivityAreaFeaturedata, SessionVariables.RequestProfile);
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
			var data = new ProductivityAreaFeatureDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

            TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProductivityAreaFeature;
            PrimaryEntityKey = "ProductivityAreaFeature";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}