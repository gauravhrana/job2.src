using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.Menu
{
    public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {
        
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.Menu;
            PrimaryEntityKey      = "Menu";
            PrimaryGenericControl = myGenericControl;
            BreadCrumbObject      = Master.BreadCrumbObject;
        }

        #endregion

    }
}