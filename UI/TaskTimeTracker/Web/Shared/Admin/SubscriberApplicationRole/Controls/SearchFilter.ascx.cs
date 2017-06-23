using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Admin.SubscriberApplicationRole.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilterEntity
    {

        #region variables

        public event EventHandler OnSearch;

        public SubscriberApplicationRoleDataModel SearchParameters
        {
            get
            {
                var data = new SubscriberApplicationRoleDataModel();

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

			BaseSearchFilterControl = SearchFilterControl;
        }

        #endregion

	}
}