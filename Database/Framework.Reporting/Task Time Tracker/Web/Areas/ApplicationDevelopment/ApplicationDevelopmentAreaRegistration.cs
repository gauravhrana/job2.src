using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.ApplicationDevelopment
{
	public class ApplicationDevelopmentAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "ApplicationDevelopment";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"ApplicationDevelopment_default",
				"AreaApplicationDevelopment/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
