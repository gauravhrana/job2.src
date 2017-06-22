using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.Scheduling
{
	public class Module
	{
		public static void Initalize()
		{
			SetupScheduleRoutes();
		}

		private static void SetupScheduleRoutes()
		{
			List<string> entityNamesSchedule = new List<string>(new string[]	{
				"CustomTimeLog","Schedule",	"ScheduleDetail",
				"ScheduleHistory","ScheduleItem", "ScheduleQuestion","ScheduleState", "VacationPlan","ScheduleView"
			});

			foreach (string entity in entityNamesSchedule)
			{
				var oApplicationRouteDataModel = new ApplicationRouteDataModel();

				oApplicationRouteDataModel.EntityName = entity;
				var route1 = string.Empty;

				var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());				

				foreach (var dataItem in items)
				{
					if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/Scheduling/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/Scheduling/{0}/{{action}}.aspx", dataItem.EntityName);
					}
					else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
					{
						dataItem.RelativeRoute = string.Format("~/BM/Scheduling/{0}/Default.aspx", dataItem.EntityName);
					}
					ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
				}
			}
		}

	}
}