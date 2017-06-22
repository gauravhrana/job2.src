﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.DataAccess;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()	
		{
			var UpdatedData = new List<ModuleOwnerDataModel>();
			var data = new ModuleOwnerDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ModuleOwnerId =
					Convert.ToInt32(SelectedData.Rows[i][ModuleOwnerDataModel.DataColumns.ModuleOwnerId].ToString());
				data.ModuleId = Convert.ToInt32(SelectedData.Rows[i][ModuleOwnerDataModel.DataColumns.ModuleId].ToString());
                data.ApplicationId =
                    Convert.ToInt32(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());
				data.FeatureOwnerStatusId = Convert.ToInt32(SelectedData.Rows[i][ModuleOwnerDataModel.DataColumns.FeatureOwnerStatusId].ToString());
				data.DeveloperRoleId = Convert.ToInt32(SelectedData.Rows[i][ModuleOwnerDataModel.DataColumns.DeveloperRoleId].ToString());
				data.TotalHoursWorked = Convert.ToInt32(SelectedData.Rows[i][ModuleOwnerDataModel.DataColumns.TotalHoursWorked].ToString());
				data.Developer =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ModuleOwnerDataModel.DataColumns.Developer))
					? CheckAndGetRepeaterTextBoxValue(ModuleOwnerDataModel.DataColumns.Developer)
					: SelectedData.Rows[i][ModuleOwnerDataModel.DataColumns.Developer].ToString();

				TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ModuleOwnerDataModel();
				data.ModuleOwnerId = Convert.ToInt32(SelectedData.Rows[i][ModuleOwnerDataModel.DataColumns.ModuleOwnerId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var moduleOwnerdata = new ModuleOwnerDataModel();
            moduleOwnerdata.ModuleOwnerId = entityKey;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetEntityDetails(moduleOwnerdata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ModuleOwner;
            PrimaryEntityKey = "ModuleOwner";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}