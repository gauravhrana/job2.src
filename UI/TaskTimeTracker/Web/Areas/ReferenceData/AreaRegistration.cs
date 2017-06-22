using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.ReferenceDataArea
{
	public class AreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "ReferenceData";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			//// Route override to work with Angularjs and HTML5 routing
			//context.MapRoute(
			//	name: "ReferenceData1Override",
			//	url: "ReferenceData/app/{*.}"
			//);

			context.MapRoute(
				"ReferenceData_default",
				"ReferenceData/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);

			// Route override to work with Angularjs and HTML5 routing
			context.MapRoute(
				name: "ReferenceData2Override",
				url: "ReferenceData/app/{*.}",
				defaults: new { action = "Index", id = UrlParameter.Optional }
			);

		}
	}
}
