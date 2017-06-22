using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Framework.Components.ApplicationUser;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.DataAccess;


namespace Shared.UI.Web.Configuration.Menu.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
    {

        public MenuDataModel SearchParameters
        {
            get
            {
                var data              = new MenuDataModel();

				SearchFilterControl.SetSearchParameters(data);

				CommonSearchParameters();

                return data;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BaseSearchFilterControl                         = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString	= LoadKendoComboBoxSources;
        }


    }
}