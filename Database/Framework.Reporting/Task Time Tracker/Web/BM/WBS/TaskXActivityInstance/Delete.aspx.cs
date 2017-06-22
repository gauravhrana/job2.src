﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.TaskXActivityInstance
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey			= "TaskXActivityInstance";
			BreadCrumbObject			= Master.BreadCrumbObject;

			var detailscontrolpath		= ApplicationCommon.GetControlPath("TaskXActivityInstance", ControlType.DetailsControl);
			DetailsControlPath			= detailscontrolpath;
			PrimaryPlaceHolder			= plcDetailsList;
			PrimaryEntity				= Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance;
		}


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new TaskXActivityInstanceDataModel();
					data.TaskXActivityInstanceId = int.Parse(index);
					TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Delete(data, SessionVariables.RequestProfile);
					DeleteAndRedirect();
				}
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskXActivityInstanceEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion
		
	}
}