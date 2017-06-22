﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageAttribute
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<FunctionalityImageAttributeDataModel>();
			var data = new FunctionalityImageAttributeDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FunctionalityImageId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageId].ToString());
				data.FunctionalityImageAttributeId =
					Convert.ToInt32(SelectedData.Rows[i][FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId].ToString());
				data.FunctionalityImage =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage))
					? CheckAndGetRepeaterTextBoxValue(FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage)
					: SelectedData.Rows[i][FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImage].ToString();

				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FunctionalityImageAttributeDataModel();
				data.FunctionalityImageAttributeId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityImageAttributeDataModel.DataColumns.FunctionalityImageAttributeId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
				
			}
			return UpdatedData.ToDataTable();
		}			

		protected override DataTable GetEntityData(int? entityKey)
		{
			var functionalityImageAttributedata = new FunctionalityImageAttributeDataModel();
			functionalityImageAttributedata.FunctionalityImageAttributeId = entityKey;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageAttributeDataManager.GetEntityDetails(functionalityImageAttributedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageAttribute;
			PrimaryEntityKey = "FunctionalityImageAttribute";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}