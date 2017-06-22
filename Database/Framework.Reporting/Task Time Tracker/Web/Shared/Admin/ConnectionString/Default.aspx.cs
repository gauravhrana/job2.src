using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Admin.ConnectionString
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
    {

		#region Methods

		protected override DataTable GetData()
		{
			var dt = Framework.Components.Core.ConnectionStringDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity          = Framework.Components.DataAccess.SystemEntity.ConnectionString;
            PrimaryEntityKey       = "ConnectionString";
            PrimaryEntityIdColumn  = "ConnectionStringId";

            MasterPageCore         = Master;
            SubMenuCore            = Master.SubMenuObject;
            BreadCrumbObject       = Master.BreadCrumbObject;

            SearchFilterCore       = oSearchFilter;
            GroupListCore          = oGroupList;

            IsDynamicSearchControl = true;

            VisibilityManagerCore  = oVC;
        }
        
        #endregion

    }
}