﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;


namespace Shared.UI.Web.Admin
{
	public partial class GridActionBarSettings : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region private methods

		private DataTable GetData()
		{
			Framework.Components.LogAndTrace.UserLoginDataModel search = new Framework.Components.LogAndTrace.UserLoginDataModel();
			search.UserName = null;
			search.UserLoginId = null;
			search.UserLoginStatusId = null;

			
			var dt = Framework.Components.LogAndTrace.UserLoginDataManager.Search(search, SessionVariables.RequestProfile);

			return dt;
		}

		private string[] GetColumns()
		{
			//if (!oList.FieldConfigurationMode.Equals(String.Empty))
			//	return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserLoginStatus, oList.FieldConfigurationMode, SessionVariables.RequestProfile);
			//else
				return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserLoginStatus, "DBColumns", SessionVariables.RequestProfile);
		}

		#endregion

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "GridActionBarSettingsDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;			
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarBackgroundColor).StartsWith("#"))
					txtBackgroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarBackgroundColor).Remove(0, 1);
				else
					txtBackgroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarBackgroundColor);
				if (PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarForegroundColor).StartsWith("#"))
					txtForegroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarForegroundColor).Remove(0, 1);
				else
					txtForegroundColor.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarForegroundColor);
				txtFontfamily.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarFontFamily);
				txtFontSize.Text = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarFontSize);
			}

			//oList.Setup("UserLogin", "Admin", "UserLoginId", true, GetData, GetColumns, "UserLogin", String.Empty);
		}

		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{
			
			string BackGroundColor = String.Empty;
			string ForeGroundColor = String.Empty;

			if (!txtBackgroundColor.Text.StartsWith("#"))
				BackGroundColor = "#" + txtBackgroundColor.Text.Trim();
			else
				BackGroundColor = txtBackgroundColor.Text.Trim();

			if (!txtForegroundColor.Text.StartsWith("#"))
				ForeGroundColor = "#" + txtForegroundColor.Text.Trim();
			else
				ForeGroundColor = txtForegroundColor.Text.Trim();

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.GridActionBarBackgroundColor, BackGroundColor);
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.GridActionBarForegroundColor, ForeGroundColor);

			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.GridActionBarFontFamily, txtFontfamily.Text.Trim());
			PreferenceUtility.UpdateApplicationUserPreference("General", ApplicationCommon.GridActionBarFontSize, txtFontSize.Text.Trim());

			Response.Redirect(Request.RawUrl);
		}

	}
}