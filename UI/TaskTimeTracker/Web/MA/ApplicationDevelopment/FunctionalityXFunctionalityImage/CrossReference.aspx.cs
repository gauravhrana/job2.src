using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{
		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);			
			
			var bcControl = this.Master.BreadCrumbObject;
			SettingCategory = "FunctionalityXFunctionalityImageDefaultView";
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup("");
			bcControl.GenerateMenu();
		}


		#endregion
	}
}