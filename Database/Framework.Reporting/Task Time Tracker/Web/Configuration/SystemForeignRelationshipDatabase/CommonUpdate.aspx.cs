﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipDatabase
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new SystemForeignRelationshipDatabaseDataModel();
			UpdatedData = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.SystemForeignRelationshipDatabaseId =
					Convert.ToInt32(SelectedData.Rows[i][SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId].ToString());

				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());


				Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Update(data, SessionVariables.RequestProfile);
				data = new SystemForeignRelationshipDatabaseDataModel();

				data.SystemForeignRelationshipDatabaseId = Convert.ToInt32(SelectedData.Rows[i][SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId].ToString());
				var dt = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var systemForeignRelationshipDatabasedata = new SystemForeignRelationshipDatabaseDataModel();
			systemForeignRelationshipDatabasedata.SystemForeignRelationshipDatabaseId = entityKey;
			var results = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Search(systemForeignRelationshipDatabasedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipDatabase;
			PrimaryEntityKey = "SystemForeignRelationshipDatabase";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	
	}
}