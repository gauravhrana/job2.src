using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.Audit.AuditHistory
{
    public partial class Details : PageDetails
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.AuditHistory;
			PrimaryEntityKey	= "AuditHistory";
			DetailsControlPath	= ApplicationCommon.GetControlPath("AuditHistory", ControlType.DetailsControl);
			PrimaryPlaceHolder	= plcDetailsList;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    }
}