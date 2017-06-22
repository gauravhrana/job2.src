using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Layer.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {        
        public LayerDataModel SearchParameters
        {
            get
            {
				var data = new LayerDataModel();

                SearchFilterControl.SetSearchParameters(data);				

				//SearchFilterControl.GetParameterValue("GroupBy");
				//SearchFilterControl.GetParameterValue("SubGroupBy");               

                return data;
            }
        }
        
        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
		}        
    }
}