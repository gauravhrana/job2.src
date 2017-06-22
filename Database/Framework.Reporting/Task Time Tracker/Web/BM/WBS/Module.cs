using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.WBS
{
	public class Module
	{
		public static void Initalize()
		{
			SetupWBSRoutes();
		}

		private static void SetupWBSRoutes()
		{
			List<string> entityNamesWBS = new List<string>(new string[]	{
				"Activity","ActivityState",	"ActivityXDeliverableArtifact",
				"DeliverableArtifact","DeliverableArtifactStatus", "FeatureXTask","Task", "TaskAlgorithm","TaskAlgorithmItem","TaskFormulation",
				"TaskNote","TaskPackage","TaskPackageXOwnerXTask","TaskPersonMapping","TaskPriorityType",
				"TaskPriorityXApplicationUser","TaskTiskRewardRankingPerson","TaskRole","TaskStatusType","TaskType","TaskXActivityInstance",
				"TaskXCompetency","TaskXDeliverableArtifact"
			});

			foreach (string entity in entityNamesWBS)
			{
				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

				oApplicationRouteDataModel.EntityName = entity;
				var route1 = string.Empty;

				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());
				switch (entity)
				{					
					case "DeliverableArtifactStatus":
						route1 = "~/BM/WBS/DeliverableArtifact/";
						break;
					//default:
					//	route1 = "~/WBS/";
					//	break;

				}

				foreach (var dataItem in items)
				{
					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/WBS/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/WBS/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/WBS/{0}/Default.aspx", dataItem.EntityName);
					}

					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
				}
			}
		}
	}
}