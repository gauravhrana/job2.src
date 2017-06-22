using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.BM.Productivity
{
	public class Module
	{
		public static void Initalize()
		{
			SetupProductivityRoutes();
		}

		private static void SetupProductivityRoutes()
		{
			List<string> entityNamesProductivity = new List<string>(new string[]	{
				"ProductivityArea", "ProductivityAreaFeature", "ProductivityAreaFeatureXApplicationUser",
				"ProductivityAreaXProductivityAreaFeature"					
			});

			foreach (string entity in entityNamesProductivity)
			{
				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

				oApplicationRouteDataModel.EntityName = entity;
				var route1 = string.Empty;

				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());

				foreach (var dataItem in items)
				{
					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/Productivity/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/Productivity/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/Productivity/{0}/Default.aspx", dataItem.EntityName);
					}
					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
				}
			}
		}
	}
}