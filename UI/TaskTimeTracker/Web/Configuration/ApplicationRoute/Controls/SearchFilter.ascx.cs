using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.ApplicationRoute.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{
		#region variables		

		public ApplicationRouteDataModel SearchParameters
		{
			get
			{
				var data = new ApplicationRouteDataModel();

				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   ApplicationRouteDataModel.DataColumns.RouteName + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(ApplicationRouteDataModel.DataColumns.RouteName) != "")
				{
					data.RouteName = CheckAndGetFieldValue(ApplicationRouteDataModel.DataColumns.RouteName).ToString();
				}

				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   ApplicationRouteDataModel.DataColumns.EntityName + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(ApplicationRouteDataModel.DataColumns.EntityName) != "")
				{
					data.EntityName = CheckAndGetFieldValue(ApplicationRouteDataModel.DataColumns.EntityName).ToString();
				}

				return data;
			}
		}

		

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.ApplicationRoute;
			PrimaryEntityKey		= "ApplicationRoute";
			FolderLocationFromRoot	= "Shared/Configuration/ApplicationRoute";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}