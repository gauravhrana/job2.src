using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
		#region variables		

		public ScheduleItemDataModel SearchParameters
		{
			get
			{
				var data = new ScheduleItemDataModel();

				data.ScheduleId = GetParameterValueAsInt(ScheduleItemDataModel.DataColumns.ScheduleId);

				data.TaskFormulationId = GetParameterValueAsInt(ScheduleItemDataModel.DataColumns.TaskFormulationId);

				return data;
			}
		}		

		#endregion		

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ScheduleItem";
			FolderLocationFromRoot = "Scheduling/ScheduleItem";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleItem;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}