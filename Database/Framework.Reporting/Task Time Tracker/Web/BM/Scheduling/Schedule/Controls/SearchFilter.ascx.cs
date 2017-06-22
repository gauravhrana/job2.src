using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Globalization;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using System.Text;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
		#region variables			

		public ScheduleDataModel SearchParameters
		{
			get
			{
				var data = new ScheduleDataModel();				
				
				SearchFilterControl.SetSearchParameters(data);

				var searchDates = SearchFilterControl.GetParameterValueForDatePanel(ScheduleDataModel.DataColumns.WorkDate);
				data.FromSearchDate = searchDates.Count > 0 ? searchDates[0] : null;
				data.ToSearchDate = searchDates.Count > 1 ? searchDates[1] : null;
								
				return data;
			}
			
		}		

		#endregion

		#region Methods

		public void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			if (fieldName.Equals("GroupBy"))
			{
				var list = new Dictionary<string, string>();

				list.Add("PersonId","Person");
				list.Add("WorkDate–Day", "WorkDate – Day");
				list.Add("WorkDate–Month", "WorkDate – Month");
				list.Add("WorkDate–Quarter", "WorkDate – Quarter");
				list.Add("WorkDate–Week", "WorkDate – Week");
				list.Add("WorkDate–Year", "WorkDate – Year");


				dropDownListControl.DataSource = list;
				dropDownListControl.DataTextField = "Value";
				dropDownListControl.DataValueField = "Key";

				dropDownListControl.DataBind();			
				

			}
			else if (fieldName.Equals("SubGroupBy"))
			{
				var list = new Dictionary<string, string>();

				list.Add("Person", "Person");
				list.Add("WorkDate–Day", "WorkDate – Day");
				list.Add("WorkDate–Month", "WorkDate – Month");
				list.Add("WorkDate–Quarter", "WorkDate – Quarter");
				list.Add("WorkDate–Week", "WorkDate – Week");
				list.Add("WorkDate–Year", "WorkDate – Year");

				dropDownListControl.DataSource = list;
				dropDownListControl.DataTextField = "Value";
				dropDownListControl.DataValueField = "Key";

				dropDownListControl.DataBind();			
			}
		}		

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);			

			base.BaseSearchFilterControl = SearchFilterControl;

			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
		}		

		#endregion

    }
}