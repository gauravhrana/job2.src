using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ClientXProject
{
	public partial class Insert : PageInsert
	{
		
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity         = SystemEntity.ClientXProject;
			PrimaryEntityKey      = "ClientXProject";
			PrimaryGenericControl = myGenericControl;
			BreadCrumbObject      = Master.BreadCrumbObject;	
		}

	}
}