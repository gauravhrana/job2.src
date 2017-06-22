using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner
{
	public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
	{
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ModuleOwner;
            PrimaryEntityKey = "ModuleOwner";
            PrimaryGenericControl = myGenericControl;
            BreadCrumbObject = Master.BreadCrumbObject;
        }

	}
}