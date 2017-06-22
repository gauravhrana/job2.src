//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using DataModel.Framework.Core;
//using Framework.Components.Core;

//namespace ApplicationContainer.UI.Web.MA
//{
//	public class Module
//	{
//		public static void Initalize()
//		{
//			SetupMARoutes();
//		}

//		private static void SetupMARoutes()
//		{
//			List<string> entityNamesAppDev = new List<string>(new string[]	{
//				"AllEntityDetail","DeveloperRole","EntityOwner","FeatureOwnerStatus","Functionality","FunctionalityActiveStatus",
//				"FunctionalityEntityStatus","FunctionalityEntityStatusArchive","FunctionalityHistory","FunctionalityImage",
//				"FunctionalityImageAttribute","FunctionalityImageInstance","FunctionalityOwner","FunctionalityPriority",
//				"FunctionalityStatus","FunctionalityXFunctionalityActiveStatus","FunctionalityXFunctionalityImage",
//				"Module","ModuleOwner","HelpPage","HelpPageContext","ApplicationOperation","ApplicationRole","ApplicationUser",
//				"ApplicationUserProfileImage","ApplicationUserProfileImageMaster","ApplicationUserTitle","RunTimeFeature"
//			});

//			foreach (string entity in entityNamesAppDev)
//			{
//				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

//				oApplicationRouteDataModel.EntityName = entity;
//				var route1 = string.Empty;

//				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());

//				switch (entity)
//				{
//					case "AllEntityDetail":
//					case "DeveloperRole":
//					case "EntityOwner":
//					case "FeatureOwnerStatus":
//					case "Functionality":
//					case "FunctionalityActiveStatus":
//					case "FunctionalityEntityStatus":
//					case "FunctionalityEntityStatusArchive":
//					case "FunctionalityHistory":
//					case "FunctionalityImage":
//					case "FunctionalityImageAttribute":
//					case "FunctionalityImageInstance":
//					case "FunctionalityOwner":
//					case "FunctionalityPriority":
//					case "FunctionalityStatus":
//					case "FunctionalityXFunctionalityActiveStatus":
//					case "FunctionalityXFunctionalityImage":
//					case "Module":
//					case "ModuleOwner":
//						route1 = "~/MA/ApplicationDevelopment/";
//						break;

//					case "HelpPage":
//					case "HelpPageContext":
//						route1 = "~/MA/ApplicationManagement/";
//						break;

//					case "ApplicationOperation":
//					case "ApplicationRole":
//					case "ApplicationUser":
//					case "ApplicationUserProfileImage":
//					case "ApplicationUserProfileImageMaster":
//					case "ApplicationUserTitle":
//						route1 = "~/MA/AuthenticationAndAuthorization/";
//						break;
//					case "RunTimeFeature":
//						route1 = "~/MA/";
//						break;
					
//				}

//				foreach (var dataItem in items)
//				{
//					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/MA/{0}/{{action}}.aspx", dataItem.EntityName);
//					}
//					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/MA/{0}/{{action}}.aspx", dataItem.EntityName);
//					}
//					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/MA/{0}/Default.aspx", dataItem.EntityName);
//					}

//					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
//				}
//			}
//		}
//	}
//}