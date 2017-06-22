using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Entity
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new EntityDataModel();

			var UpdatedData = new List<EntityDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.EntityId = Convert.ToInt32(SelectedData.Rows[i][EntityDataModel.DataColumns.EntityId].ToString());
				data.WorkTicket = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.WorkTicket)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.WorkTicket)) : int.Parse(SelectedData.Rows[i][EntityDataModel.DataColumns.WorkTicket].ToString());
				data.WorkTicketId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.WorkTicketId)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.WorkTicketId)) : int.Parse(SelectedData.Rows[i][EntityDataModel.DataColumns.WorkTicketId].ToString());
				data.Application = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.Application)) ? CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.Application) : SelectedData.Rows[i][EntityDataModel.DataColumns.Application].ToString();
				data.Name = SelectedData.Rows[i][EntityDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.Description) : SelectedData.Rows[i][EntityDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][EntityDataModel.DataColumns.SortOrder].ToString());

				EntityDataManager.Update(data, SessionVariables.RequestProfile);

				data = new EntityDataModel();

				data.EntityId = Convert.ToInt32(SelectedData.Rows[i][EntityDataModel.DataColumns.EntityId].ToString());

				var dt = EntityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new EntityDataModel();
			data.EntityId = entityKey;
			var results = EntityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.Entity;		
				PrimaryEntityKey	= "Entity";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
