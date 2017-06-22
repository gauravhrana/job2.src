using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Framework.Components.LogAndTrace;
using MvcApplication3;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using Framework.Components.Core;
using System.Data;
using System.Web.SessionState;
using System.Web.Services.Protocols;

namespace ApplicationContainer.UI.Web
{

    public class Global : HttpApplication
    {

		public static RequestProfile GetStartupProfile()
		{
			var appId = int.Parse(ConfigurationManager.AppSettings["StartupApplicationId"]);
			var systemAuditId = ApplicationCommon.GetSystemAuditId("PMT");
			var startupProfile = new RequestProfile(systemAuditId, SessionVariables.ApplicationMode, appId);
			return startupProfile;
		}

		public static void RegisterRoutes(RouteCollection routes)
        {
			Log4Net.LogInfo("RegisterRoutes Start");

            routes.Ignore("{resource}.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // fixes issues regarding Web Service call from javascript (scripts -> library -> kendo.js)
            routes.Add(new Route("API/{resource}.asmx/{*pathInfo}", new StopRoutingHandler()));

			RouteConfig.RegisterRoutes(RouteTable.Routes);

            // 1-1 mapping of home pages for each .net application
            routes.MapPageRoute("PMTHomeRoute",
                "PMT/PMT/Home",
                "~/BM/PMO/Default.aspx",
                true);

            routes.MapPageRoute("TEHomeRoute",
                "TE/TE/Home",
                "~/BM/Scheduling/Default.aspx",
                true);

            routes.MapPageRoute("PDTMGMDEVTHomeRoute",
                "PDTMGMDEVT/PDTMGMDEVT/Home",
                "~/BM/RequirementAnalysis/Default.aspx",
                true);

            routes.MapPageRoute("PrototypeHomeRoute",
                "Prototype/Prototype/Home",
                "~/Prototype/Default.aspx",
                true);

            routes.MapPageRoute("SAHomeRoute",
                "SA/SA/Home",
                "~/Shared/SA/Default.aspx",
                true);

			//routes.MapPageRoute("LegalHomeRoute",
			//	"Legal/Home",
			//	"~/BM/Legal/Default.aspx",
			//	true);

			var systemRequestProfile = GetStartupProfile();

			var dtRoutes = ApplicationRouteDataManager.GetList(systemRequestProfile);
			var dtParameters = ApplicationRouteParameterDataManager.GetList(systemRequestProfile);

			// add routes which maps default pages to search first
            var searchRouteRows = dtRoutes.Select("RouteName LIKE '%EntityRouteSearch'");
            if (searchRouteRows.Length > 0)
            {
                foreach (DataRow dr in searchRouteRows)
                {
                    var proposedRoute = "{applicationCode}/{applicationModule}/" + Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.ProposedRoute]);
                    var relativeRoute = Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.RelativeRoute]);
                    var routeName = Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.RouteName]);

                    // MapPageRoute provides a way to define routes for Web Forms applications.
                    routes.MapPageRoute(routeName, proposedRoute, relativeRoute, true);
                }
            }

			// add other routes
			 searchRouteRows = dtRoutes.Select("RouteName NOT LIKE '%EntityRouteSearch'");
			if (searchRouteRows.Length > 0)
            {
				foreach (DataRow dr in searchRouteRows)
                {
                    var proposedRoute = "{applicationCode}/{applicationModule}/" + Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.ProposedRoute]);
					var relativeRoute	= Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.RelativeRoute]);
					var appRouteId		= Convert.ToInt32(dr[ApplicationRouteDataModel.DataColumns.ApplicationRouteId]);
					var routeName		= Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.RouteName]);

                    var routeParamList = new RouteValueDictionary();
					var routeParameters = dtParameters.Select(ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId + " = " + appRouteId);

					if (routeParameters.Length > 0)
                    {
                        foreach (DataRow drParam in routeParameters)
                        {
							var key = drParam[ApplicationRouteParameterDataModel.DataColumns.ParameterName];
							var value = drParam[ApplicationRouteParameterDataModel.DataColumns.ParameterValue];

							routeParamList.Add(key.ToString(), value.ToString());
                        }

                       routeParamList.Add("applicationCode", "PMT");
                       //routeParamList.Add("applicationModule", "AA");
                    }

                    // MapPageRoute provides a way to define routes for Web Forms applications.
                    routes.MapPageRoute(routeName, proposedRoute, relativeRoute, true, routeParamList);
                }
            }

			//routes.MapRoute(
			//	name: "Default",
			//	url: "{controller}/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
			//	namespaces: new[] { "Areas.Controllers" }
			//);

			//// Single Page Routing Example of a test page
			//routes.MapPageRoute("testRoute",
			//	"Test/TestPaging",
			//	"~/Shared/ApplicationManagement/Development/TestPaging/Test.aspx",
			//	true);

			//routes.MapPageRoute("testAng",
			//	"Test/Home",
			//	"~/Test/Home/Index.cshtml",
			//	false);

			//routes.MapPageRoute("TestPage1",
			//	"Page1",
			//	"~/Areas/AngularTest/app/app.js",
			//	false);

			//routes.MapPageRoute("d", "abcd", "~/Areas/AngularTest/app/core/views/nav.html", false);

			Log4Net.LogInfo("RegisterRoutes End");

        }

		// Code that runs on application startup
        private void Application_Start(object sender, EventArgs e)
        {
            //var validatiorsConfigFile = ConfigurationSettings.AppSettings["DynamicValidatorsConfigurationFile"];

			// why PMT?
			var systemAuditId = ApplicationCommon.GetSystemAuditId("PMT");

			SetupConfiguration.SetConnectionList(systemAuditId);
            SetupConfiguration.UserMachineName = Environment.MachineName;

			Log4Net.LogInfo("Initalize_Modules");

			//PMO.Module.Initalize();
			//MS.Module.Initalize();
			//BM.Module.Initalize();
			//WBS.Module.Initalize();
			//Scheduling.Module.Initalize();
			//MA.Module.Initalize();
            //ApplicationContainer.UI.Web.BM.TCM.Module.Initalize();
            //RequirementAnalysis.Module.Initalize();

			Log4Net.LogInfo("Application_Start");

			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterRoutes(RouteTable.Routes);

			//System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Net20WebFormsApplication.Models.Net20WebFormsApplicationContext>());
        }

		protected void Application_PostAuthorizeRequest()
		{
			HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
		}

		// Code that runs when an unhandled error occurs
        private void Application_Error(object sender, EventArgs e)
        {
			ApplicationCommon.SendLastErrorInEmail(Server.MachineName);
		}

        private void Session_Start(object sender, EventArgs e)
        {
            Log4Net.LogInfo("Session_Start");

			var applicationId = int.Parse(ConfigurationManager.AppSettings["PMT.ApplicationId"]);

            SessionVariables.CurrentApplicationCode = "PMT";
            SessionVariables.CurrentApplicationModuleCode = "PMT";

			SessionVariables.SystemRequestProfile = new RequestProfile(ApplicationCommon.GetSystemAuditId("PMT"), SessionVariables.ApplicationMode, applicationId);

			SessionVariables.RequestProfile = new RequestProfile(WebApplicationUser.GetCurrentUserId(applicationId), SessionVariables.ApplicationMode, applicationId);

            SessionVariables.UserAuthorized = WebApplicationUser.CheckIfUserIsValid(SessionVariables.RequestProfile.AuditId);
            SessionVariables.TopNCount      = 5;

            // Need to revisit this IsTesting logic whether We need this at all?
            SessionVariables.IsTesting = !(SessionVariables.UserApplicationMode > 0);

            Log4NetDataManager.Cleanup(5, SessionVariables.RequestProfile);
        }

		// Code that runs when a session ends.
		// Note: The Session_End event is raised only when the sessionstate mode
		// is set to InProc in the Web.config file. If session mode is set to StateServer
		// or SQLServer, the event is not raised.
        private void Session_End(object sender, EventArgs e)
        {
            //Session.Remove("ConnectionKeyList");
        }
    }
}
