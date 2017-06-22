using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.EntityDateRangeStateType
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new EntityDateRangeStateTypeDataModel();

			var UpdatedData = new List<EntityDateRangeStateTypeDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.EntityDateRangeStateTypeId = Convert.ToInt32(SelectedData.Rows[i][EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId].ToString());
				data.Name = SelectedData.Rows[i][EntityDateRangeStateTypeDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateTypeDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateTypeDataModel.DataColumns.Description) : SelectedData.Rows[i][EntityDateRangeStateTypeDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateTypeDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateTypeDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][EntityDateRangeStateTypeDataModel.DataColumns.SortOrder].ToString());

				EntityDateRangeStateTypeDataManager.Update(data, SessionVariables.RequestProfile);

				data = new EntityDateRangeStateTypeDataModel();

				data.EntityDateRangeStateTypeId = Convert.ToInt32(SelectedData.Rows[i][EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId].ToString());

				var dt = EntityDateRangeStateTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new EntityDateRangeStateTypeDataModel();
			data.EntityDateRangeStateTypeId = entityKey;
			var results = EntityDateRangeStateTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.EntityDateRangeStateType;		
				PrimaryEntityKey	= "EntityDateRangeStateType";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
