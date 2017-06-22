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
	public partial class InlineUpdate : PageInlineUpdate
	{
		#region Methods

		protected override DataTable GetData()
		{
			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

				var selectedrows = new DataTable();
				var CustomTimeLogdata = new CustomTimeLogDataModel();

				selectedrows = CustomTimeLogDataManager.GetDetails(CustomTimeLogdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						CustomTimeLogdata.CustomTimeLogId = entityKey;
						var result = CustomTimeLogDataManager.GetDetails(CustomTimeLogdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}

				}
				else
				{
					CustomTimeLogdata.CustomTimeLogId = SetId;
					var result = CustomTimeLogDataManager.GetDetails(CustomTimeLogdata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);

				}
				return selectedrows;
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
			return null;
		}

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new CustomTimeLogDataModel();

			if (values.ContainsKey(CustomTimeLogDataModel.DataColumns.CustomTimeLogId))
			{
				data.CustomTimeLogId = int.Parse(values[CustomTimeLogDataModel.DataColumns.CustomTimeLogId].ToString());
			}

			if (values.ContainsKey(CustomTimeLogDataModel.DataColumns.PersonId))
			{
				data.PersonId = int.Parse(values[CustomTimeLogDataModel.DataColumns.PersonId].ToString());
			}

			if (values.ContainsKey(CustomTimeLogDataModel.DataColumns.PromotedDate))
			{
				data.PromotedDate = DateTime.Parse(values[CustomTimeLogDataModel.DataColumns.PromotedDate].ToString());
			}
			

			if (values.ContainsKey(CustomTimeLogDataModel.DataColumns.Value))
			{
				data.Value = decimal.Parse(values[CustomTimeLogDataModel.DataColumns.Value].ToString());
			}

			
			CustomTimeLogDataManager.Update(data, SessionVariables.RequestProfile);
			InlineEditingList.Data = GetData();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			PrimaryEntity = SystemEntity.CustomTimeLog;
			PrimaryEntityKey = "CustomTimeLog";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion

	}
}