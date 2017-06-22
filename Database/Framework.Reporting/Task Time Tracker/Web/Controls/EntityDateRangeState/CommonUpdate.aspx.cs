using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.EntityDateRangeState
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new EntityDateRangeStateDataModel();
            UpdatedData = EntityDateRangeStateDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.EntityDateRangeStateId =
					Convert.ToInt32(SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateId].ToString());

				data.EntityDateRangeStateTypeId =
					Convert.ToInt32(SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateTypeId].ToString());

				data.StartDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.StartDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.StartDate))
					: DateTime.Parse(SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.StartDate].ToString());

				data.EndDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.EndDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.EndDate))
					: DateTime.Parse(SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.EndDate].ToString());

				data.KeyId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.KeyId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.KeyId))
					: int.Parse(SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.KeyId].ToString());

				data.SystemEntityId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.SystemEntityId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.SystemEntityId))
					: int.Parse(SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.SystemEntityId].ToString());

				data.Notes =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.Notes))
					? CheckAndGetRepeaterTextBoxValue(EntityDateRangeStateDataModel.DataColumns.Notes)
					: SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.Notes].ToString();

                EntityDateRangeStateDataManager.Update(data, SessionVariables.RequestProfile);
				data = new EntityDateRangeStateDataModel();
				data.EntityDateRangeStateId = Convert.ToInt32(SelectedData.Rows[i][EntityDateRangeStateDataModel.DataColumns.EntityDateRangeStateId].ToString());
                var dt = EntityDateRangeStateDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var entityDateRangeStatedata = new EntityDateRangeStateDataModel();
			entityDateRangeStatedata.EntityDateRangeStateId = entityKey;
            var results = EntityDateRangeStateDataManager.Search(entityDateRangeStatedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.EntityDateRangeState;
			PrimaryEntityKey = "EntityDateRangeState";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}