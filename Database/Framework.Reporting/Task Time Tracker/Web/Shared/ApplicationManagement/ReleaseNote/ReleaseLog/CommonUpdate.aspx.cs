using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new ReleaseLogDataModel();
			UpdatedData = Framework.Components.ReleaseLog.ReleaseLogDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ReleaseLogId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDataModel.DataColumns.ReleaseLogId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.ReleaseLogStatusId = Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDataModel.DataColumns.ReleaseLogStatusId].ToString());					

				data.VersionNo = SelectedData.Rows[i][ReleaseLogDataModel.DataColumns.VersionNo].ToString();

				data.ReleaseDate = Convert.ToDateTime(SelectedData.Rows[i][ReleaseLogDataModel.DataColumns.ReleaseDate].ToString());


				Framework.Components.ReleaseLog.ReleaseLogDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ReleaseLogDataModel();
				data.ReleaseLogId = Convert.ToInt32(SelectedData.Rows[i][ReleaseLogDataModel.DataColumns.ReleaseLogId].ToString());
				var dt = Framework.Components.ReleaseLog.ReleaseLogDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var releaseLogData = new ReleaseLogDataModel();
			releaseLogData.ReleaseLogId = entityKey;
			var results = Framework.Components.ReleaseLog.ReleaseLogDataManager.Search(releaseLogData, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLog;
			PrimaryEntityKey = "ReleaseLog";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}