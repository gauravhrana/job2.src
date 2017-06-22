using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;
			PrimaryEntityKey   = "FieldConfigurationModeXApplicationRole";
			DetailsControlPath = ApplicationCommon.GetControlPath("FieldConfigurationModeXApplicationRole", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
			BreadCrumbObject   = Master.BreadCrumbObject;
		}

		#endregion 

    }
}