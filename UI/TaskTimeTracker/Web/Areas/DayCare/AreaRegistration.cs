using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.DayCareArea
{
	public class AreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "DayCare";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			//// Route override to work with Angularjs and HTML5 routing
			//context.MapRoute(
			//	name: "DayCare1Override",
			//	url: "DayCare/app/{*.}"
			//);

			context.MapRoute(
				"DayCare_default",
				"DayCare/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);

			// Route override to work with Angularjs and HTML5 routing
			context.MapRoute(
				name: "DayCare2Override",
				url: "DayCare/app/{*.}",
				defaults: new { action = "Index", id = UrlParameter.Optional }
			);

		}
	}
}
