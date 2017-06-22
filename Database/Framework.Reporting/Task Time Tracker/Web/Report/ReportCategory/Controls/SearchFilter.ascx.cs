using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Core;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ReportCategory.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilterEntity
    {

        public ReportCategoryDataModel SearchParameters
        {
            get
            {
                var data = new ReportCategoryDataModel();

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