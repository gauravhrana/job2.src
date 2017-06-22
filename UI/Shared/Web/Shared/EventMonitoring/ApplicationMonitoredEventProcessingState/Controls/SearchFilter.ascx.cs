using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;
using DataModel.Framework.DataAccess;

namespace Shared.UI.Web.ApplicationMonitoredEventProcessingState.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public ApplicationMonitoredEventProcessingStateDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationMonitoredEventProcessingStateDataModel();
			
				SearchFilterControl.GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

                return data;
            }
        }

	    private DataTable SearchColumns
	    {
		    get
		    {
			    if (ViewState["ValidSearchColumns"] != null)
			    {
				    return (DataTable) ViewState["ValidSearchColumns"];
			    }
			    return null;
		    }
		    set { ViewState["ValidSearchColumns"] = value; }
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