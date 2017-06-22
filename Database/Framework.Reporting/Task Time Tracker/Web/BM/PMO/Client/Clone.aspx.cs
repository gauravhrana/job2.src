using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;


namespace ApplicationContainer.UI.Web.PMO.Client
{
	public partial class Clone : PageClone
    {
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.Client;
			PrimaryEntityKey		= "Client";
			BreadCrumbObject		= Master.BreadCrumbObject;
			GenericControlPath		= ApplicationCommon.GetControlPath("Client", ControlType.GenericControl);
			PrimaryGenericControl	= myGenericControl;			
		}
	
        #endregion

    }
}