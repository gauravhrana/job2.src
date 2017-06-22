﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<FieldConfigurationModeXApplicationRoleDataModel>();
			var data = new FieldConfigurationModeXApplicationRoleDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.FieldConfigurationModeXApplicationRoleId =
					Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeXApplicationRoleId].ToString());

				data.ApplicationRoleId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId).ToString())
					: int.Parse(SelectedData.Rows[i][FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId].ToString());

				data.FieldConfigurationModeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId).ToString())
					: int.Parse(SelectedData.Rows[i][FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId].ToString());

				FieldConfigurationModeXApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new FieldConfigurationModeXApplicationRoleDataModel();
				data.FieldConfigurationModeXApplicationRoleId = Convert.ToInt32(SelectedData.Rows[i][FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeXApplicationRoleId].ToString());
				var dt = FieldConfigurationModeXApplicationRoleDataManager.GetEntityList(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
				
			}
			return UpdatedData.ToDataTable();
		}

		protected override string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns("DBColumns", Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole, SessionVariables.RequestProfile);
		}			

		protected override DataTable GetEntityData(int? entityKey)
		{
			var FieldConfigurationModeXApplicationRoledata = new FieldConfigurationModeXApplicationRoleDataModel();
			FieldConfigurationModeXApplicationRoledata.FieldConfigurationModeXApplicationRoleId = entityKey;
			var results = FieldConfigurationModeXApplicationRoleDataManager.GetEntityList(FieldConfigurationModeXApplicationRoledata, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;
			PrimaryEntityKey = "FieldConfigurationModeXApplicationRole";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
		
	}
}