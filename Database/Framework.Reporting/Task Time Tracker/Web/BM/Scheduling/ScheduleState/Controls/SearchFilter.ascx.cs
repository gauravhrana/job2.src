using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Collections;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using ApplicationContainer.UI.Web.BaseUI;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;


namespace ApplicationContainer.UI.Web.Scheduling.ScheduleState.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables

		public ScheduleStateDataModel SearchParameters
		{
			get
			{
				var data = new ScheduleStateDataModel();

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