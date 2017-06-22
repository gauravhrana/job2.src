using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket
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

				var selectedrows = new List<WorkTicketDataModel>();
				var entitydata = new WorkTicketDataModel();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						entitydata.EntityId = entityKey;
						var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetDetails(entitydata, SessionVariables.RequestProfile);
						selectedrows.Add(result);
					}
				}
				else
				{
					entitydata.EntityId = SetId;
					var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.GetDetails(entitydata, SessionVariables.RequestProfile);
					selectedrows.Add(result);

				}
				return selectedrows.ToDataTable();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}

			return null;
		}

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new WorkTicketDataModel();

			// copies properties from values dictionary object to data object
			PropertyMapper.CopyProperties(data, values);

			TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.WorkTicket;
			PrimaryEntityKey = "WorkTicket";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion

	}
}

