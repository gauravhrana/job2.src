using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.Theme
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
	{

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Theme;
			PrimaryEntityKey = "Theme";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("Theme", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}

	}
}