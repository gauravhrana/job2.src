using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.UserPreference;
using System.Globalization;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Scheduling.VacationPlan.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

		public VacationPlanDataModel SearchParameters
		{
			get
			{
				var data               = new VacationPlanDataModel();
				//DateTime? tempDate		   ;
				
				SearchFilterControl.SetSearchParameters(data);

				var startDates       = SearchFilterControl.GetParameterValueForDatePanel(VacationPlanDataModel.DataColumns.StartDate);
				var endDates		 = SearchFilterControl.GetParameterValueForDatePanel(VacationPlanDataModel.DataColumns.EndDate);
			
				if (startDates.Count != 0)
				{
					data.StartDate = startDates.Count > 0 ? startDates[0] : null;
					data.StartDateR2 = startDates.Count > 1 ? startDates[1] : null;
				}


				if (endDates.Count != 0)
				{
					data.EndDate = endDates.Count > 0 ? endDates[0] : null;
					data.EndDateR2 = endDates.Count > 1 ? endDates[1] : null;
				}

				if (startDates.Count != 0 && endDates.Count != 0)
				{
					if ((endDates[0] <= startDates[0]) || (endDates[1] < startDates[1]))
					{
						SearchFilterControl.SwitchColumnValues(VacationPlanDataModel.DataColumns.StartDate, VacationPlanDataModel.DataColumns.EndDate);

						var tmpDate = data.StartDate;
						var tmpDateR = data.StartDateR2;

						data.StartDate = data.EndDate;
						data.StartDateR2 = data.EndDateR2;

						data.EndDate = data.StartDate;
						data.EndDateR2 = data.StartDateR2;
					}
					
				}
				CommonSearchParameters();

				return data;
			}
		}	

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			BaseSearchFilterControl							= SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}

    }
}