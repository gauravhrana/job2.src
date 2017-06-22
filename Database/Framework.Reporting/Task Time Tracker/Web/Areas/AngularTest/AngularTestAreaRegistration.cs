using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.AngularTest
{
	public class AngularTestAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "AngularTest";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			//// Route override to work with Angularjs and HTML5 routing
			//context.MapRoute(
			//	name: "Application1Override",
			//	url: "AngularTest/app/{*.}"
			//);

			context.MapRoute(
				"AngularTest_default",
				"AngularTest/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);

			// Route override to work with Angularjs and HTML5 routing
			context.MapRoute(
				name: "Application1Override",
				url: "AngularTest/app/{*.}",
				defaults: new { action = "Index", id = UrlParameter.Optional }
			);

		}
	}
}
