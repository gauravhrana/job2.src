using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.RequirementAnalysis
{
	public class Module
	{
		static Module()
		{

		}

		public static void Initalize()
		{
			//SetupClientRoute();
			SetupRARoutes();
		}

		private static void SetupRARoutes()
		{
			List<string> entityNamesRA = new List<string>(new string[]	{
				"Feature","FeatureGroup", "FeatureGroupXFeature", "FeatureRule", "FeatureRuleCategory", "FeatureRuleStatus",
				"FeatureXFeatureRule", "Need","NeedXFeature", "ProjectUseCaseStatus", "ProjectUseCaseStatusArchieve",
				"ProjectXNeed", "ProjectXUseCase", "UseCase", "UseCaseActor", "UseCaseActorXUseCase", 
				"UseCasePackage", "UseCasePackageXUseCase", "UseCaseRelationship", "UseCaseStep",
				"UseCaseWorkFlowCategory", "UseCaseXUseCaseStep"
			});

			foreach (string entity in entityNamesRA)
			{
				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

				oApplicationRouteDataModel.EntityName = entity;
				var route1 = string.Empty;

				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());
				switch (entity)
				{
					case "FeatureGroup":
					case "FeatureGroupXFeature":
					case "FeatureRule":
					case "FeatureRuleCategory":
					case "FeatureRuleStatus":
					case "FeatureXFeatureRule":
						route1 = "~/BM/RequirementAnalysis/Feature/";
						break;

				}

				foreach (var dataItem in items)
				{
					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/RequirementAnalysis/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/RequirementAnalysis/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/RequirementAnalysis/{0}/Default.aspx", dataItem.EntityName);
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
					dataItem.RelativeRoute = string.Format("~/RequirementAnalysis/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
				{
					dataItem.RelativeRoute = string.Format("~/RequirementAnalysis/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
				{
					dataItem.RelativeRoute = string.Format("~/RequirementAnalysis/{0}/Default.aspx", dataItem.EntityName);
				}

				ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
			}
		}
	}
}