using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Admin.SubscriberApplicationRole
{
	public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
	{
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.SubscriberApplicationRole;
            PrimaryEntityKey = "SubscriberApplicationRole";
            PrimaryGenericControl = myGenericControl;
            BreadCrumbObject = Master.BreadCrumbObject;
        }
	}
}