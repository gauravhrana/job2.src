﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
	{
		#region private methods

		protected override DataTable GetData()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask;
			PrimaryEntityKey        = "TaskPackageXOwnerXTask";
			PrimaryEntityIdColumn   = "TaskPackageXOwnerXTaskId";

			MasterPageCore          = Master;
			SubMenuCore             = Master.SubMenuObject;
			BreadCrumbObject        = Master.BreadCrumbObject;

			SearchFilterCore		= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);		
			GroupListCore           = oGroupList;

            IsDynamicSearchControl  = true;

			VisibilityManagerCore = oVC;
		}		

		#endregion

	}
}