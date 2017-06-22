using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Configuration.ThemeKey
{
    public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeKey;
            PrimaryEntityKey = "ThemeKey";
            PrimaryGenericControl = myGenericControl;
            BreadCrumbObject = Master.BreadCrumbObject;
        }
    }
}