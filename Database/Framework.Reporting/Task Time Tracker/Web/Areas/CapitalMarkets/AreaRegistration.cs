using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.CapitalMarketsArea
{
	public class AreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "CapitalMarkets";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			//// Route override to work with Angularjs and HTML5 routing
			//context.MapRoute(
			//	name: "CapitalMarkets1Override",
			//	url: "CapitalMarkets/app/{*.}"
			//);

			context.MapRoute(
				"CapitalMarkets_default",
				"CapitalMarkets/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);

			// Route override to work with Angularjs and HTML5 routing
			context.MapRoute(
				name: "CapitalMarkets2Override",
				url: "CapitalMarkets/app/{*.}",
				defaults: new { action = "Index", id = UrlParameter.Optional }
			);

		}
	}
}
