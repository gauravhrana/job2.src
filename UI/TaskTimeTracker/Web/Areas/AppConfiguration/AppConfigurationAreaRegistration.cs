using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.AppConfigurationArea
{
	public class AppConfigurationAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "AppConfiguration";
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
				"AppConfiguration_default",
				"AppConfiguration/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);

			// Route override to work with Angularjs and HTML5 routing
			context.MapRoute(
				name: "Application2Override",
				url: "AppConfiguration/app/{*.}",
				defaults: new { action = "Index", id = UrlParameter.Optional }
			);

		}
	}
}
