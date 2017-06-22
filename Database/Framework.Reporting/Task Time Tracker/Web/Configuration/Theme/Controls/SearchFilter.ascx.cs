using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.UserPreference;
using System.Data;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using ApplicationContainer.UI.Web.BaseUI;

namespace Shared.UI.Web.Configuration.Theme.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{

		#region variables

		public ThemeDataModel SearchParameters
		{
			get
			{
				var data = new ThemeDataModel();

				//if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				//	ThemeDataModel.DataColumns.Name + "Visibility", SettingCategory)
				//	&& !string.IsNullOrEmpty(CheckAndGetFieldValue(
				//		ThemeDataModel.DataColumns.Name).ToString()))
				//{
				//	data.Name = CheckAndGetFieldValue(
				//	   ThemeDataModel.DataColumns.Name).ToString();
				//}
				SearchFilterControl.GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

				CommonSearchParameters();

				return data;
			}
		}
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;

		}

		#endregion
	}
}