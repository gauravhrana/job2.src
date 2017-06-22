using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.LegalArea
{
	public class AreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Legal";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			//// Route override to work with Angularjs and HTML5 routing
			//context.MapRoute(
			//	name: "Legal1Override",
			//	url: "Legal/app/{*.}"
			//);

			context.MapRoute(
				"Legal_default",
				"Legal/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);

			// Route override to work with Angularjs and HTML5 routing
			context.MapRoute(
				name: "Legal2Override",
				url: "Legal/app/{*.}",
				defaults: new { action = "Index", id = UrlParameter.Optional }
			);

		}
	}
}
