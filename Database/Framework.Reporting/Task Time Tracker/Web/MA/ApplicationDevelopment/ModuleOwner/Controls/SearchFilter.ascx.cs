using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;
using DataModel.Framework.AuthenticationAndAuthorization;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner.Controls
{
	public partial class SearchFilter : ControlSearchFilterEntity
	{
        #region variables

        public ModuleOwnerDataModel SearchParameters
        {
            get
            {
                var data = new ModuleOwnerDataModel();

                data.Developer = SearchFilterControl.GetParameterValue(ModuleOwnerDataModel.DataColumns.Developer);

                data.ModuleId = SearchFilterControl.GetParameterValueAsInt(ModuleOwnerDataModel.DataColumns.ModuleId);

                data.DeveloperRoleId = SearchFilterControl.GetParameterValueAsInt(ModuleOwnerDataModel.DataColumns.DeveloperRoleId);

				data.FeatureOwnerStatusId = SearchFilterControl.GetParameterValueAsInt(ModuleOwnerDataModel.DataColumns.FeatureOwnerStatusId);

                data.ApplicationId = SearchFilterControl.GetParameterValueAsInt(ModuleOwnerDataModel.DataColumns.ApplicationId);

				CommonSearchParameters();

                return data;
            }
        }

		

		#endregion

		#region Methods

		//public void SetGroupBy()
		//{
		//	GroupBy = CheckAndGetFieldValue("GroupBy", false).ToString();
		//	GroupByDirection = CheckAndGetFieldValue("GroupByDirection", false).ToString();
		//}

		//public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		//{
		//	base.LoadDropDownListSources(fieldName, dropDownListControl);

		//	if (fieldName.Equals("Module"))
		//	{
		//		var moduleData = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);
		//		UIHelper.LoadDropDown(moduleData, dropDownListControl,
		//			StandardDataModel.StandardDataColumns.Name,
		//		ModuleDataModel.DataColumns.ModuleId);
		//	}

		//	if (fieldName.Equals("DeveloperRole"))
		//	{
		//		var drData = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);
		//		UIHelper.LoadDropDown(drData, dropDownListControl,
		//			StandardDataModel.StandardDataColumns.Name,
		//		DeveloperRoleDataModel.DataColumns.DeveloperRoleId);
		//	}

		//	if (fieldName.Equals("FeatureOwnerStatus"))
		//	{
		//		var drData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.GetList(SessionVariables.RequestProfile);
		//		UIHelper.LoadDropDown(drData, dropDownListControl,
		//			StandardDataModel.StandardDataColumns.Name,
		//		FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId);
		//	}

		//	if (fieldName.Equals("ApplicationId"))
		//	{
		//		var drData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
		//		UIHelper.LoadDropDown(drData, dropDownListControl,
		//			StandardDataModel.StandardDataColumns.Name,
		//		ApplicationDataModel.DataColumns.ApplicationId);
		//	}
		//}


		//public override string LoadKendoComboBoxSources(string fieldName, TextBox txtBox, PlaceHolder plcControlHolder)
		//{
		//	if (fieldName.Equals("ApplicationId"))
		//	{
		//		return AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", plcControlHolder);
		//	}
		//	if (fieldName.Equals("DeveloperRole"))
		//	{
		//		return AjaxHelper.GetKendoComboBoxConfigScript("GetDeveloperRoleList", "Name", "DeveloperRoleId", plcControlHolder);
		//	}
		//	if (fieldName.Equals("FeatureOwnerStatus"))
		//	{
		//		return AjaxHelper.GetKendoComboBoxConfigScript("GetFeatureOwnerStatusList", "Name", "FeatureOwnerStatusId", plcControlHolder);
		//	}
		//	if (fieldName.Equals("Module"))
		//	{
		//		return AjaxHelper.GetKendoComboBoxConfigScript("GetModuleList", "Name", "ModuleId", plcControlHolder);
		//	}

		//	return string.Empty;

		//}
		
		#endregion

		#region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			BaseSearchFilterControl = SearchFilterControl;
			SearchFilterControl.GetKendoComboBoConfigString = LoadKendoComboBoxSources;
        }

        #endregion

	}
}