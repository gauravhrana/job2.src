using System;
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

namespace Shared.UI.Web.Configuration.SystemForeignRelationship
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new SystemForeignRelationshipDataModel();
			UpdatedData = Framework.Components.Core.SystemForeignRelationshipDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{				
				data.SystemForeignRelationshipId =
					Convert.ToInt32(SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipId].ToString());

				data.PrimaryDatabaseId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId))
					: int.Parse(SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.PrimaryDatabaseId].ToString());

				data.ForeignDatabaseId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId))
					: int.Parse(SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.ForeignDatabaseId].ToString());

				data.PrimaryEntityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.PrimaryEntityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.PrimaryEntityId).ToString())
					: int.Parse(SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.PrimaryEntityId].ToString());

				data.ForeignEntityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.ForeignEntityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.ForeignEntityId).ToString())
					: int.Parse(SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.ForeignEntityId].ToString());

				data.FieldName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.FieldName))
					? CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.FieldName).ToString()
					: SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.FieldName].ToString();

				data.Source =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.Source))
					? CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.Source).ToString()
					: SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.Source].ToString();

				data.SystemForeignRelationshipTypeId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipTypeId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipTypeId).ToString())
					: int.Parse(SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipTypeId].ToString());

				Framework.Components.Core.SystemForeignRelationshipDataManager.Update(data, SessionVariables.RequestProfile);
				data = new SystemForeignRelationshipDataModel();

				data.SystemForeignRelationshipId = Convert.ToInt32(SelectedData.Rows[i][SystemForeignRelationshipDataModel.DataColumns.SystemForeignRelationshipId].ToString());
				var dt = Framework.Components.Core.SystemForeignRelationshipDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var systemForeignRelationshipdata = new SystemForeignRelationshipDataModel();
			systemForeignRelationshipdata.SystemForeignRelationshipId = entityKey;
			var results = Framework.Components.Core.SystemForeignRelationshipDataManager.Search(systemForeignRelationshipdata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationship;
			PrimaryEntityKey = "SystemForeignRelationship";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	
       
	}
}