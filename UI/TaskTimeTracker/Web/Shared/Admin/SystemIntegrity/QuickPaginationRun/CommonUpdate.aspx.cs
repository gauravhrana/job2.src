using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Dapper;

namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun
{
	public partial class CommonUpdate : PageCommonUpdate
	{

		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<QuickPaginationRunDataModel>();
			var data = new QuickPaginationRunDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.QuickPaginationRunId =
					Convert.ToInt32(SelectedData.Rows[i][QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId].ToString());

				data.ApplicationUserName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(QuickPaginationRunDataModel.DataColumns.ApplicationUserName))
					? CheckAndGetRepeaterTextBoxValue(QuickPaginationRunDataModel.DataColumns.ApplicationUserName)
					: SelectedData.Rows[i][QuickPaginationRunDataModel.DataColumns.ApplicationUserName].ToString();
				data.SystemEntityType =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(QuickPaginationRunDataModel.DataColumns.SystemEntityType))
					? CheckAndGetRepeaterTextBoxValue(QuickPaginationRunDataModel.DataColumns.SystemEntityType)
					: SelectedData.Rows[i][QuickPaginationRunDataModel.DataColumns.SystemEntityType].ToString();

				

				QuickPaginationRunDatatManager.Update(data, SessionVariables.RequestProfile);
				data = new QuickPaginationRunDataModel();

				data.QuickPaginationRunId = Convert.ToInt32(SelectedData.Rows[i][QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId].ToString());

                //var dt = QuickPaginationRunDatatManager.Gete(data, SessionVariables.RequestProfile);

                //if (dt.Rows.Count == 1)
                //{
                //    UpdatedData.Add(dt.Rows[0]);
                //}
			}

			return UpdatedData.ToDataTable();
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var QuickPaginationRunData = new QuickPaginationRunDataModel();
			QuickPaginationRunData.QuickPaginationRunId = entityKey;
			var results = QuickPaginationRunDatatManager.Search(QuickPaginationRunData, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.QuickPaginationRun;
			PrimaryEntityKey = "QuickPaginationRun";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}