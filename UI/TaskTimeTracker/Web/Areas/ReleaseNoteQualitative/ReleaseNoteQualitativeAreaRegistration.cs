using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.ReleaseNoteQualitative
{
	public class ReleaseNoteQualitativeAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "ReleaseNoteQualitative";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			// MapRoute is meant for "normal" ASP.NET MVC controllers
			context.MapRoute(
				"ReleaseNoteQualitative_default",
				"ReleaseNoteQualitative/{controller}/{action}/{id}/{isMultiple}",
				new { action = "Index", id = UrlParameter.Optional, isMultiple = UrlParameter.Optional },
				new[] { "ApplicationContainer.UI.Web.Areas.ReleaseNoteQualitative.Controllers" } 
			);

            // this route is added to map with our current URL implementation
            context.MapRoute(
                "ReleaseNoteQualitative_default1",
                "{applicationCode}/{applicationModule}/ReleaseNoteQualitative/{controller}/{action}/{id}/{isMultiple}",
                new { action = "Index", id = UrlParameter.Optional, isMultiple = UrlParameter.Optional },
                new[] { "ApplicationContainer.UI.Web.Areas.ReleaseNoteQualitative.Controllers" }
            );

            //context.MapRoute(
            //    "Schedule_default2",
            //    "{applicationCode}/{applicationModule}/AreaSchedule/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
		}
	}
}
