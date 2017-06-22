using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationManagement.HelpPage
{
    public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
    {

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.HelpPage;
			PrimaryEntityKey = "HelpPage";
			PrimaryGenericControl = myGenericControl;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

    }
}