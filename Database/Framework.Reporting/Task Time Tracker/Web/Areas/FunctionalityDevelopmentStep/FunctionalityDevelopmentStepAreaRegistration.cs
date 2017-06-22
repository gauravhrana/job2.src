using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.FunctionalityDevelopmentStep
{
	public class FunctionalityDevelopmentStepAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "FunctionalityDevelopmentStep";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"FunctionalityDevelopmentStep_default",
				"FunctionalityDevelopmentStep/{controller}/{action}/{id}/{isMultiple}",
				new { action = "Index", id = UrlParameter.Optional, isMultiple = UrlParameter.Optional }
			);
		}
	}
}