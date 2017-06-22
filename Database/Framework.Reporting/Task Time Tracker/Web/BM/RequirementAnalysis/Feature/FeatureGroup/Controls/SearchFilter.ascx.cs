using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.BaseUI;

namespace ApplicationContainer.UI.Web.FeatureGroup.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        #region variables

        public FeatureGroupDataModel SearchParameters
        {
            get
            {
                var data = new FeatureGroupDataModel();

                SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

                return data;
            }
        }

        #endregion

        #region Events       
        
        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl; ;
        }

        #endregion
    }
}