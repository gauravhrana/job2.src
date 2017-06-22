using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace Shared.UI.Web.Configuration.UserPreferenceCategory.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        #region variables

        public UserPreferenceCategoryDataModel SearchParameters
        {
            get
            {
                var data = new UserPreferenceCategoryDataModel();

                SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();
				
                return data;
            }
        }

        #endregion

        #region methods

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