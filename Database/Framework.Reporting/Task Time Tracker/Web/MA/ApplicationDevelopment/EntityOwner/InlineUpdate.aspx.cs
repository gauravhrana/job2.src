using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner
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
				var EntityOwnerdata = new EntityOwnerDataModel();

				selectedrows = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.GetDetails(EntityOwnerdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						EntityOwnerdata.EntityOwnerId = entityKey;
						var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.GetDetails(EntityOwnerdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					EntityOwnerdata.EntityOwnerId = SetId;
					var result = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.GetDetails(EntityOwnerdata, SessionVariables.RequestProfile);
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
			var data = new EntityOwnerDataModel();

			// copies properties from values dictionary object to data object
			PropertyMapper.CopyProperties(data, values);

			TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityOwner;
			PrimaryEntityKey = "EntityOwner";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion
	}
}