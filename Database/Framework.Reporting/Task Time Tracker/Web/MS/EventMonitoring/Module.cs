using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.MS.EventMonitoring
{
	public class Module
	{
		public static void Initalize()
		{
			SetupEMRoutes();
		}

		private static void SetupEMRoutes()
		{
			List<string> entityNamesAuthAndAuth = new List<string>(new string[] { 
				"ApplicationMonitoredEvent","ApplicationMonitoredEventEmail",
				"ApplicationMonitoredEventProcessingState","ApplicationMonitoredEventSource"		
			});

			foreach (string entity in entityNamesAuthAndAuth)
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
							dataItem.RelativeRoute = string.Format("~/MS/EventMonitoring/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/MS/EventMonitoring/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						if (route1 != string.Empty)
							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
						else
							dataItem.RelativeRoute = string.Format("~/MS/EventMonitoring/{0}/Default.aspx", dataItem.EntityName);
					}

					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
				}
			}
		}
	}
}