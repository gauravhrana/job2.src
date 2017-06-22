using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.ApplicationMonitoredEventEmail.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {

        #region variables       

        public ApplicationMonitoredEventEmailDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationMonitoredEventEmailDataModel();

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
			if (fieldName.Equals("ApplicationUser"))
			{
				var applicationUserdata = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationUserdata, dropDownListControl,
					ApplicationUserDataModel.DataColumns.FirstName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);

			}
		}		

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ApplicationMonitoredEventEmail";
			FolderLocationFromRoot = "Shared/EventMonitoring/ApplicationMonitoredEventEmail";
			PrimaryEntity = SystemEntity.ApplicationMonitoredEventEmail;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion   
        
    }
}