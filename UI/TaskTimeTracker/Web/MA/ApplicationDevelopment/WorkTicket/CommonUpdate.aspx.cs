using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<WorkTicketDataModel>();
			var data = new WorkTicketDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.EntityId =
					Convert.ToInt32(SelectedData.Rows[i][WorkTicketDataModel.DataColumns.EntityId].ToString());

				data.WorkTicketId =
					Convert.ToInt32(SelectedData.Rows[i][WorkTicketDataModel.DataColumns.WorkTicketId].ToString());


				data.Name =
					 !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name))
					 ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Name)
					 : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();

				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.Update(data, SessionVariables.RequestProfile);
				data = new WorkTicketDataModel();
				data.WorkTicketId = Convert.ToInt32(SelectedData.Rows[i][WorkTicketDataModel.DataColumns.WorkTicketId].ToString());
				var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var entitydata = new WorkTicketDataModel();
			entitydata.EntityId = entityKey;
			var results = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetEntityDetails(entitydata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.WorkTicket;
			PrimaryEntityKey = "WorkTicket";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}