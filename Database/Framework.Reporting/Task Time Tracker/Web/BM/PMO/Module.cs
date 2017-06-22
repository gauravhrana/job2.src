using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.PMO
{
	public class Module
	{
		static Module()
		{

		}

		public static void Initalize()
		{
			//SetupClientRoute();
			SetupPMORoutes();
		}

		private static void SetupPMORoutes()
		{
			List<string> entityNamesPMO = new List<string>(new string[]	{
                "Client", "ClientXProject", "Layer", "Milestone",
				"MilestoneFeatureState", "MilestoneXFeature", "MilestoneXFeatureArchive",
				"Project", "ProjectTimeLine",
				"ProjectPortfolio", "ProjectPortfolioGroup", "ProjectPortfolioGroupXProjectPortfolio"
			});

			foreach (string entity in entityNamesPMO)
			{
				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

				oApplicationRouteDataModel.EntityName = entity;
				var route1 = string.Empty;

				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());
                switch(entity)
                {
                    case "Client":
                    case "ClientXProject":
                    case "Layer":
                    case "Milestone":
                        route1 = "~/BM/PMO/";
                        break;

                    case "MilestoneFeatureState":
                    case "MilestoneXFeature":
                    case "MilestoneXFeatureArchive":
                        route1 = "~/BM/PMO/Milestone/";
                        break;

                    case "Project":
                    case "ProjectTimeLine":
                         route1 = "~/BM/PMO/ProjectManagement/";
                        break;
                    
                    case "ProjectPortfolio":
                    case "ProjectPortfolioGroup":
                    case "ProjectPortfolioGroupXProjectPortfolio":
                        route1 = "~/BM/PMO/ProjectManagement/Project/";
                        break;

                }
									 
				foreach (var dataItem in items)
				{
					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
					{
						if(route1!=string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/BM/PMO/{0}/Default.aspx", dataItem.EntityName);
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
						dataItem.RelativeRoute = string.Format("~/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						dataItem.RelativeRoute = string.Format("~/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						dataItem.RelativeRoute = string.Format("~/PMO/{0}/Default.aspx", dataItem.EntityName);
					}

					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
				}			
		}
	}
}