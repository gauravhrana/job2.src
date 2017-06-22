using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.ApplicationMonitoredEvent.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {
        #region variables                

        public ApplicationMonitoredEvenDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationMonitoredEvenDataModel();
			

                return data;

            }
        }		

        #endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ApplicationMonitoredEventSource"))
			{
				var applicationMonitoredEventSourcedata = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationMonitoredEventSourcedata, dropDownListControl,
					ApplicationMonitoredEventSourceDataModel.DataColumns.Code,
					ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId);

			}

		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ApplicationMonitoredEventSource";
			FolderLocationFromRoot = "Shared/EventMonitoring/ApplicationMonitoredEventSource";
			PrimaryEntity = SystemEntity.ApplicationMonitoredEventSource;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
        
    }
}