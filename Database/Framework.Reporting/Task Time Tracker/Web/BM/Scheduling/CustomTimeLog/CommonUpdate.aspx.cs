using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new CustomTimeLogDataModel();
			UpdatedData = CustomTimeLogDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.CustomTimeLogId =
					Convert.ToInt32(SelectedData.Rows[i][CustomTimeLogDataModel.DataColumns.CustomTimeLogId].ToString());

				data.PersonId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(CustomTimeLogDataModel.DataColumns.PersonId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(CustomTimeLogDataModel.DataColumns.PersonId).ToString())
					: int.Parse(SelectedData.Rows[i][CustomTimeLogDataModel.DataColumns.PersonId].ToString());

				data.PromotedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(CustomTimeLogDataModel.DataColumns.PromotedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(CustomTimeLogDataModel.DataColumns.PromotedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][CustomTimeLogDataModel.DataColumns.PromotedDate].ToString());

				

				data.Value =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(CustomTimeLogDataModel.DataColumns.Value))
					? decimal.Parse(CheckAndGetRepeaterTextBoxValue(CustomTimeLogDataModel.DataColumns.Value).ToString())
					: decimal.Parse(SelectedData.Rows[i][CustomTimeLogDataModel.DataColumns.Value].ToString());

				
				CustomTimeLogDataManager.Update(data, SessionVariables.RequestProfile);
				data = new CustomTimeLogDataModel();
				data.CustomTimeLogId = Convert.ToInt32(SelectedData.Rows[i][CustomTimeLogDataModel.DataColumns.CustomTimeLogId].ToString());
				var dt = CustomTimeLogDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var CustomTimeLogdata = new CustomTimeLogDataModel();
			CustomTimeLogdata.CustomTimeLogId = entityKey;
			var results = CustomTimeLogDataManager.Search(CustomTimeLogdata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.CustomTimeLog;
			PrimaryEntityKey = "CustomTimeLog";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion


	}
}