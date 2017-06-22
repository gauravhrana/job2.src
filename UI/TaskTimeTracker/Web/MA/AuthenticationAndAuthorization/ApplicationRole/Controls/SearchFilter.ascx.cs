using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.DataAccess;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilterEntity
    {

        public ApplicationRoleDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationRoleDataModel();

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