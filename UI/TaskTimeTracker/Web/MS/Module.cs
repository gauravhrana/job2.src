//using System;	
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using DataModel.Framework.Core;
//using Framework.Components.Core;

//namespace ApplicationContainer.UI.Web.MS
//{
//	public class Module
//	{
//		public static void Initalize()
//		{
//			SetupMSRoutes();
//		}

//		private static void SetupMSRoutes()
//		{
//			List<string> entityNamesAppDev = new List<string>(new string[]	{
//				"ApplicationMonitoredEvent","ApplicationMonitoredEventEmail","ApplicationMonitoredEventProcessingState",
//				"ApplicationMonitoredEventSource","NotificationEventType","NotificationPublisher",
//				"NotificationPublisherXEventType","NotificationRegistrar","NotificationSubscriber",
//				"TaskEntity","TaskEntityType","TaskRun","TaskSchedule","TaskScheduleType"
//			});

//			foreach (string entity in entityNamesAppDev)
//			{
//				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

//				oApplicationRouteDataModel.EntityName = entity;
//				var route1 = string.Empty;

//				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());

//				switch (entity)
//				{
//					case "ApplicationMonitoredEvent":
//					case "ApplicationMonitoredEventEmail":
//					case "ApplicationMonitoredEventProcessingState":
//					case "ApplicationMonitoredEventSource":
//						route1 = "~/MS/EventMonitoring/";
//						break;

//					case "NotificationEventType":
//					case "NotificationPublisher":
//					case "NotificationPublisherXEventType":
//					case "NotificationRegistrar":
//					case "NotificationSubscriber":
//						route1 = "~/MS/EventNotification/";
//						break;

//					case "TaskEntity":
//					case "TaskEntityType":
//					case "TaskRun":
//					case "TaskSchedule":
//					case "TaskScheduleType":
//						route1 = "~/MS/TasksAndWorkflow/";
//						break;					
//				}

//				foreach (var dataItem in items)
//				{
//					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/MS/{0}/{{action}}.aspx", dataItem.EntityName);
//					}
//					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/MS/{0}/{{action}}.aspx", dataItem.EntityName);
//					}
//					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
//					{
//						if (route1 != string.Empty)
//							dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
//						else
//							dataItem.RelativeRoute = string.Format("~/MS/{0}/Default.aspx", dataItem.EntityName);
//					}

//					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
//				}
//			}
//		}
//	}
//}