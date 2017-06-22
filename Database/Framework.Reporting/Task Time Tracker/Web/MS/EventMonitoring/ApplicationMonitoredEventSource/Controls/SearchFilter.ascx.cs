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
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace Shared.UI.Web.ApplicationMonitoredEventSource.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public ApplicationMonitoredEventSourceDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationMonitoredEventSourceDataModel();

				SearchFilterControl.GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

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