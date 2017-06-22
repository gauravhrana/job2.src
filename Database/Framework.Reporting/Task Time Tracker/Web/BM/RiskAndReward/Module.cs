using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;


namespace ApplicationContainer.UI.Web.BM.RiskAndReward
{
	public class Module
	{
		public static void Initalize()
		{
			SetupRiskAndRewardRoutes();
		}

		private static void SetupRiskAndRewardRoutes()
		{
			List<string> entityNamesRiskAndReward = new List<string>(new string[]	{
				"Reward","Risk"
			});

			foreach (string entity in entityNamesRiskAndReward)
			{
				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

				oApplicationRouteDataModel.EntityName = entity;
				var route1 = string.Empty;

				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());

				foreach (var dataItem in items)
				{
					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/RiskAndReward/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/RiskAndReward/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/RiskAndReward/{0}/Default.aspx", dataItem.EntityName);
					}
					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
				}
			}
		}
	}
}