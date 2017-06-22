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

namespace Shared.UI.Web.Admin.BatchFileStatus
{
    public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
    {
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new BatchFileStatusDataModel();
			UpdatedData = Framework.Components.Import.BatchFileStatusDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.BatchFileStatusId =
				   Convert.ToInt32(SelectedData.Rows[i][BatchFileStatusDataModel.DataColumns.BatchFileStatusId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();
				
				data.SortOrder =
				   !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
				   ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
				   : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());


				Framework.Components.Import.BatchFileStatusDataManager.Update(data, SessionVariables.RequestProfile);
				data = new BatchFileStatusDataModel();
				data.BatchFileStatusId = Convert.ToInt32(SelectedData.Rows[i][BatchFileStatusDataModel.DataColumns.BatchFileStatusId].ToString());
				var dt = Framework.Components.Import.BatchFileStatusDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var fileTypedata = new BatchFileStatusDataModel();
			fileTypedata.BatchFileStatusId = entityKey;
			var results = Framework.Components.Import.BatchFileStatusDataManager.Search(fileTypedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileStatus;
			PrimaryEntityKey = "BatchFileStatus";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion      
    }
}