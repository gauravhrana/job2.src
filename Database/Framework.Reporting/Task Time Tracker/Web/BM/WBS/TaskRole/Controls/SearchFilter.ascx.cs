using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.WBS.TaskRole.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		#region variables

		public TaskRoleDataModel SearchParameters
		{
			get
			{
				var data = new TaskRoleDataModel();

                SearchFilterControl.SetSearchParameters(data);

				return data;
			}
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
		}

		#endregion
	}
}