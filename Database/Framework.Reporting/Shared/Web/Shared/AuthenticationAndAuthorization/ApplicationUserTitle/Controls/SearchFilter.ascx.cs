using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserTitle.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {
        public ApplicationUserTitleDataModel SearchParameters
        {
            get
            {
                var data = new ApplicationUserTitleDataModel();

                SearchFilterControl.SetSearchParameters(data);

                CommonSearchParameters();

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