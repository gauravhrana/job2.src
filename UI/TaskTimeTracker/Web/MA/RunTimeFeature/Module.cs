using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.MA.RunTimeFeature
{
	public class Module
	{
		public static void Initalize()
		{
			SetupRunTimeFeaturetRoute();			
		}


		private static void SetupRunTimeFeaturetRoute()
		{
			var oApplicationRouteDataModel = new ApplicationRouteDataModel();

			oApplicationRouteDataModel.EntityName = "RunTimeFeature";

			var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());

			foreach (var dataItem in items)
			{
				if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
				{
					dataItem.RelativeRoute = string.Format("~/MA/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
				{
					dataItem.RelativeRoute = string.Format("~/MA/{0}/{{action}}.aspx", dataItem.EntityName);
				}
				else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
				{
					dataItem.RelativeRoute = string.Format("~/MA/{0}/Default.aspx", dataItem.EntityName);
				}

				ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
			}
		}
	}	
}