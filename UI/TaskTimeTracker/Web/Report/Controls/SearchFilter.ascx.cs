using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Report.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {     

        public ReportDataModel SearchParameters
        {
            get
            {
                var data = new ReportDataModel();

                SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

                return data;
            }
        }		

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl                         = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }
		
    }
}