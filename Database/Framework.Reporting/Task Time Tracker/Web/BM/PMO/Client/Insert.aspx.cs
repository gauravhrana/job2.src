using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Client
{
	public partial class Insert : PageInsert
    {        
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.Client;
			PrimaryEntityKey		= "Client";			
            PrimaryGenericControl	= myGenericControl;			
			BreadCrumbObject		= Master.BreadCrumbObject;			
		}

        #endregion
    }
}