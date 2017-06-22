using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage
{
	public partial class FunctionalityView : Shared.UI.WebFramework.BasePage
	{
		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "FunctionalityXFunctionalityImageDefaultView";
			

		}
			

		#endregion
	}
}