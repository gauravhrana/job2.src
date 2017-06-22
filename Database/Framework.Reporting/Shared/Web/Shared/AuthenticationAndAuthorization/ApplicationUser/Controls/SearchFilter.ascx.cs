using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {
        
        public ApplicationUserDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationUserDataModel();

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