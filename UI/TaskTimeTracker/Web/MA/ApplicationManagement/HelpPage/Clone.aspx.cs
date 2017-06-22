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
    public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {       

        #region Events      

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.HelpPage;
			PrimaryEntityKey = "HelpPage";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("HelpPage", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}

        #endregion

    }
}