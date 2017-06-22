using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfiguration
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "FieldConfiguration";
			var detailsPath = ApplicationCommon.GetControlPath("FieldConfiguration", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfiguration;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}
		#endregion
	}
}