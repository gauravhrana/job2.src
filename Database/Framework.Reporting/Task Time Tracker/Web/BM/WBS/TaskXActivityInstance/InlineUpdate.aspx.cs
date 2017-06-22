using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.WBS.TaskXActivityInstance
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
				var taskXActivityInstancedata = new TaskXActivityInstanceDataModel();

				selectedrows = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.GetDetails(taskXActivityInstancedata, SessionVariables.RequestProfile).Clone();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						taskXActivityInstancedata.TaskXActivityInstanceId = entityKey;
						var result = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.GetDetails(taskXActivityInstancedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					taskXActivityInstancedata.TaskXActivityInstanceId = SetId;
					var result = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.GetDetails(taskXActivityInstancedata, SessionVariables.RequestProfile);
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
			var data = new TaskXActivityInstanceDataModel();

			PropertyMapper.CopyProperties(data, values);

			TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance;
			PrimaryEntityKey		= "TaskXActivityInstance";

			InlineEditingListCore	= InlineEditingList;
			BreadCrumbObject		= Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion
		
	}
}