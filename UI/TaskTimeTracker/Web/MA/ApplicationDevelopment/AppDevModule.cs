using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment
{
	public class AppDevModule
	{
		static AppDevModule()
		{

		}

		public static void Initalize()
		{
			//SetupClientRoute();
			SetupApplicationDevelopmentRoutes();
		}

		private static void SetupApplicationDevelopmentRoutes()
		{
			List<string> entityNamesAppDev = new List<string>(new string[]	{
				"AllEntityDetail","DeveloperRole","EntityOwner","FeatureOwnerStatus","Functionality","FunctionalityActiveStatus",
				"FunctionalityEntityStatus","FunctionalityEntityStatusArchive","FunctionalityHistory","FunctionalityImage",
				"FunctionalityImageAttribute","FunctionalityImageInstance","FunctionalityOwner","FunctionalityPriority",
				"FunctionalityStatus","FunctionalityXFunctionalityActiveStatus","FunctionalityXFunctionalityImage",
				"Module","ModuleOwner"
			});

			foreach (string entity in entityNamesAppDev)
			{
				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

				oApplicationRouteDataModel.EntityName = entity;
				var route1 = string.Empty;

				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());
				
				foreach (var dataItem in items)
				{
					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/MA/ApplicationDevelopment/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/MA/ApplicationDevelopment/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/MA/ApplicationDevelopment/{0}/Default.aspx", dataItem.EntityName);
					}

					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
				}
			}
		}


		private static void SetupClientRoute()
		{
			var oApplicationRouteDataModel = new ApplicationRouteDataModel();

			oApplicationRouteDataModel.EntityName = "Client";

			var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());

			foreach (var dataItem in items)
			{
				if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
				{
					dataItem.RelativeRoute = string.Format("~/MA/ApplicationDevelopment/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
				{
					dataItem.RelativeRoute = string.Format("~/MA/ApplicationDevelopment/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
				{
					dataItem.RelativeRoute = string.Format("~/MA/ApplicationDevelopment/{0}/Default.aspx", dataItem.EntityName);
				}

				ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
			}
		}
	}
}