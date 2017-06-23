using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.Import;
using Dapper;

namespace Shared.UI.Web.Admin.BatchFileSet
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<BatchFileSetDataModel>();
			var data = new BatchFileSetDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.BatchFileSetId =
				   Convert.ToInt32(SelectedData.Rows[i][BatchFileSetDataModel.DataColumns.BatchFileSetId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.CreatedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BatchFileSetDataModel.DataColumns.CreatedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(BatchFileSetDataModel.DataColumns.CreatedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][BatchFileSetDataModel.DataColumns.CreatedDate].ToString());

				data.CreatedByPersonId =
				   !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BatchFileSetDataModel.DataColumns.CreatedByPersonId))
				   ? int.Parse(CheckAndGetRepeaterTextBoxValue(BatchFileSetDataModel.DataColumns.CreatedByPersonId).ToString())
				   : int.Parse(SelectedData.Rows[i][BatchFileSetDataModel.DataColumns.CreatedByPersonId].ToString());


				Framework.Components.Import.BatchFileSetDataManager.Update(data, SessionVariables.RequestProfile);
				data = new BatchFileSetDataModel();
				data.BatchFileSetId = Convert.ToInt32(SelectedData.Rows[i][BatchFileSetDataModel.DataColumns.BatchFileSetId].ToString());
				var dt = Framework.Components.Import.BatchFileSetDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var fileTypedata = new BatchFileSetDataModel();
			fileTypedata.BatchFileSetId = entityKey;
			var results = Framework.Components.Import.BatchFileSetDataManager.GetEntityDetails(fileTypedata, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileSet;
			PrimaryEntityKey = "BatchFileSet";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}